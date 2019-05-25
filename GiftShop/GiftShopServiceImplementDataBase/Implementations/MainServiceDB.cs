using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopModel;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceDAL.ViewModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Net;
using System.Net.Mail;

namespace GiftShopServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private GiftDbContext context;

        public MainServiceDB(GiftDbContext context)
        {
            this.context = context;
        }

        public List<ProcedureViewModel> GetList()
        {
            List<ProcedureViewModel> result = context.Procedures.Select(rec =>
            new ProcedureViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                SetId = rec.SetId,
                SellerId = rec.SellerId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
                SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                CustomerFIO = rec.Customer.CustomerFIO,
                SetName = rec.Set.SetName,
                SellerFIO = rec.Seller.SellerFIO
            })
            .ToList();
            return result;
        }
        public List<ProcedureViewModel> GetFreeProcedures()
        {
            List<ProcedureViewModel> result = context.Procedures
                .Where(x => x.Status == ProcedureStatus.Принят || x.Status ==
                ProcedureStatus.НедостаточноРесурсов)
                .Select(rec => new ProcedureViewModel
                {
                    Id = rec.Id
                })
                .ToList();
            return result;
        }

        public void CreateProcedure(ProcedureBindingModel model)
        {
            var procedure = new Procedure
            {
                CustomerId = model.CustomerId,
                SetId = model.SetId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = ProcedureStatus.Принят
            };
            context.Procedures.Add(procedure);
            context.SaveChanges();

            var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerId);
            SendEmail(customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от " +
                "{1} создан успешно", procedure.Id, procedure.DateCreate.ToShortDateString()));
        }

        public void TakeProcedureInWork(ProcedureBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                Procedure element = context.Procedures.FirstOrDefault(rec => rec.Id == model.Id); try
                {
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != ProcedureStatus.Принят && element.Status != ProcedureStatus.НедостаточноРесурсов)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var productParts = context.SetParts
                        .Include(rec => rec.Part)
                        .Where(rec => rec.SetId == element.SetId);
                    // списываем   
                    foreach (var productPart in productParts)
                    {
                        int countOnStorages = productPart.Count * element.Count;
                        var stockParts = context.StorageParts.Where(rec =>
                        rec.PartId == productPart.PartId);
                        foreach (var stockPart in stockParts)
                        {
                            // компонентов на одном слкаде может не хватать  
                            if (stockPart.Count >= countOnStorages)
                            {
                                stockPart.Count -= countOnStorages;
                                countOnStorages = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStorages -= stockPart.Count;
                                stockPart.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStorages > 0)
                        {
                            throw new Exception("Не достаточно компонента " + productPart.Part.PartName + " требуется "
                                + productPart.Count + ", не хватает " + countOnStorages);
                        }
                    }
                    element.SellerId = model.SellerId;
                    element.DateImplement = DateTime.Now;
                    element.Status = ProcedureStatus.Выполняется;
                    context.SaveChanges();
                    SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передеан в работу", element.Id, element.DateCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    element.Status = ProcedureStatus.НедостаточноРесурсов;
                    context.SaveChanges();
                    transaction.Commit();
                    throw;
                }
            }
        }

        public void FinishProcedure(ProcedureBindingModel model)
        {
            Procedure element = context.Procedures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != ProcedureStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = ProcedureStatus.Готов;
            context.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передан на оплату", element.Id, element.DateCreate.ToShortDateString()));
        }

        public void PayProcedure(ProcedureBindingModel model)
        {
            Procedure element = context.Procedures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != ProcedureStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = ProcedureStatus.Оплачен;
            context.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} оплачен успешно", element.Id, element.DateCreate.ToShortDateString()));
        }

        public void PutPartOnStorage(StoragePartBindingModel model)
        {
            StoragePart element = context.StorageParts.FirstOrDefault(rec =>
            rec.StorageId == model.StorageId && rec.PartId == model.PartId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StorageParts.Add(new StoragePart
                {
                    StorageId = model.StorageId,
                    PartId = model.PartId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;

            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject; objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false; objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"], ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}