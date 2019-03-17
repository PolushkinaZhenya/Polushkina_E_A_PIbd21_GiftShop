using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopModel;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceDAL.ViewModel;
namespace GiftShopServiceImplementDataBase.Implementations
{
    public class SetServiceDB : ISetService
    {
        private GiftDbContext context;

        public SetServiceDB(GiftDbContext context)
        {
            this.context = context;
        }

        public List<SetViewModel> GetList()
        {
            List<SetViewModel> result = context.Sets.Select(rec => new SetViewModel
            {
                Id = rec.Id,
                SetName = rec.SetName,
                Price = rec.Price,
                SetParts = context.SetParts
                    .Where(recPC => recPC.SetId == rec.Id)
                    .Select(recPC => new SetPartViewModel
                    {
                        Id = recPC.Id,
                        SetId = recPC.SetId,
                        PartId = recPC.PartId,
                        PartName = recPC.Part.PartName,
                        Count = recPC.Count
                    })
                        .ToList()
            }).ToList(); return result;
        }

        public SetViewModel GetElement(int id)
        {
            Set element = context.Sets.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SetViewModel
                {
                    Id = element.Id,
                    SetName = element.SetName,
                    Price = element.Price,
                    SetParts = context.SetParts
                    .Where(recPC => recPC.SetId == element.Id)
                    .Select(recPC => new SetPartViewModel
                    {
                        Id = recPC.Id,
                        SetId = recPC.SetId,
                        PartId = recPC.PartId,
                        PartName = recPC.Part.PartName,
                        Count = recPC.Count
                    })
                    .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SetBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Set element = context.Sets.FirstOrDefault(rec =>
                    rec.SetName == model.SetName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Set
                    {
                        SetName = model.SetName,
                        Price = model.Price
                    };
                    context.Sets.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам 
                    var groupParts = model.SetParts
                        .GroupBy(rec => rec.PartId)
                        .Select(rec => new
                        {
                            PartId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                    // добавляем компоненты  
                    foreach (var groupPart in groupParts)
                    {
                        context.SetParts.Add(new SetPart
                        {
                            SetId = element.Id,
                            PartId = groupPart.PartId,
                            Count = groupPart.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;

                }
            }
        }

        public void UpdElement(SetBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Set element = context.Sets.FirstOrDefault(rec =>
                    rec.SetName == model.SetName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Sets.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.SetName = model.SetName;
                    element.Price = model.Price;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты 
                    var compIds = model.SetParts.Select(rec => rec.PartId).Distinct();
                    var updateParts = context.SetParts.Where(rec =>
                    rec.SetId == model.Id && compIds.Contains(rec.PartId));
                    foreach (var updatePart in updateParts)
                    {
                        updatePart.Count = model.SetParts.FirstOrDefault(rec =>
                        rec.Id == updatePart.Id).Count;
                    }
                    context.SaveChanges();
                    context.SetParts.RemoveRange(context.SetParts.Where(rec =>
                    rec.SetId == model.Id && !compIds.Contains(rec.PartId)));
                    context.SaveChanges();
                    // новые записи  
                    var groupParts = model.SetParts.Where(rec =>
                    rec.Id == 0).GroupBy(rec => rec.PartId).Select(rec => new
                    {
                        PartId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    foreach (var groupPart in groupParts)

                    {
                        SetPart elementPC = context.SetParts.FirstOrDefault(rec =>
                        rec.SetId == model.Id && rec.PartId == groupPart.PartId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupPart.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.SetParts.Add(new SetPart
                            {
                                SetId = model.Id,
                                PartId = groupPart.PartId,
                                Count = groupPart.Count
                            }); context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Set element = context.Sets.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {                         // удаяем записи по компонентам при удалении изделия  
                        context.SetParts.RemoveRange(context.SetParts.Where(rec =>
                        rec.SetId == id));
                        context.Sets.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}
