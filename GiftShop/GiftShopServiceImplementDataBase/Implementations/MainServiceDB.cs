using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopModel;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceDAL.ViewModel;

using System.Data.Entity;
using System.Data.Entity.SqlServer;

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
                SetName = rec.Set.SetName
            })
            .ToList();
            return result;
        }

        public void CreateProcedure(ProcedureBindingModel model)
        {
            context.Procedures.Add(new Procedure
            {
                CustomerId = model.CustomerId,
                SetId = model.SetId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = ProcedureStatus.Принят
            });
            context.SaveChanges();
        }

        public void TakeProcedureInWork(ProcedureBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Procedure element = context.Procedures.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != ProcedureStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var setParts = context.SetParts.Include(rec =>
                    rec.Part).Where(rec => rec.SetId == element.SetId);
                    // списываем   
                    foreach (var setPart in setParts)
                    {
                        int countOnStorages = setPart.Count * element.Count;
                        var storageParts = context.StorageParts.Where(rec =>
                        rec.PartId == setPart.PartId);
                        foreach (var storagePart in storageParts)
                        {
                            // компонентов на одном слкаде может не хватать  
                            if (storagePart.Count >= countOnStorages)
                            {
                                storagePart.Count -= countOnStorages;
                                countOnStorages = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStorages -= storagePart.Count;
                                storagePart.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStorages > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                setPart.Part.PartName + " требуется " + setPart.Count + ", не хватает " + countOnStorages);
                        }
                    }
                    element.DateImplement = DateTime.Now;
                    element.Status = ProcedureStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        public void FinishProcedure(ProcedureBindingModel model)
        {
            Procedure element = context.Procedures.FirstOrDefault(rec =>
            rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != ProcedureStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = ProcedureStatus.Готов; context.SaveChanges();
        }

        public void PayProcedure(ProcedureBindingModel model)
        {
            Procedure element = context.Procedures.FirstOrDefault(rec =>
            rec.Id == model.Id); if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != ProcedureStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = ProcedureStatus.Оплачен;
            context.SaveChanges();
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
    }
}
