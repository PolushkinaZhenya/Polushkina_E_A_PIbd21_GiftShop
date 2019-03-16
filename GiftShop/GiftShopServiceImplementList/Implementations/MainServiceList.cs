using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopModel;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceDAL.ViewModel;

namespace GiftShopServiceImplementList.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ProcedureViewModel> GetList() { List<ProcedureViewModel> result = source.Procedures.Select(rec => new ProcedureViewModel
        { Id = rec.Id,
            CustomerId = rec.CustomerId,
            SetId = rec.SetId,
            DateCreate = rec.DateCreate.ToLongDateString(),
            DateImplement = rec.DateImplement?.ToLongDateString(),
            Status = rec.Status.ToString(),
            Count = rec.Count,
            Sum = rec.Sum,
            CustomerFIO = source.Customers.FirstOrDefault(recC => recC.Id == rec.CustomerId)?.CustomerFIO,
            SetName = source.Sets.FirstOrDefault(recP => recP.Id == rec.SetId)?.SetName, }).ToList(); return result; }

        public void CreateProcedure(ProcedureBindingModel model)
        { int maxId = source.Procedures.Count > 0 ? source.Procedures.Max(rec => rec.Id) : 0;
            source.Procedures.Add(new Procedure
            { Id = maxId + 1,
                CustomerId = model.CustomerId,
                SetId = model.SetId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = ProcedureStatus.Принят }); }

        public void TakeProcedureInWork(ProcedureBindingModel model)
        {
            Procedure element = source.Procedures.FirstOrDefault(rec => rec.Id == model.Id); if (element == null) { throw new Exception("Элемент не найден"); }
            if (element.Status != ProcedureStatus.Принят) { throw new Exception("Заказ не в статусе \"Принят\""); }
            // смотрим по количеству компонентов на складах      
            var setParts = source.SetParts.Where(rec => rec.SetId == element.SetId); 
            foreach (var setPart in setParts)
            { int countOnStorages = source.StorageParts.Where(rec => rec.PartId == setPart.PartId).Sum(rec => rec.Count);
                if (countOnStorages < setPart.Count * element.Count) { var componentName = source.Parts.FirstOrDefault(rec => rec.Id == setPart.PartId);
                    throw new Exception("Не достаточно компонента " + componentName?.PartName + " требуется " + (setPart.Count * element.Count) + ", в наличии " + countOnStorages);
                }
            }             // списываем   
            foreach (var setPart in setParts)
            {
                int countOnStorages = setPart.Count * element.Count;
                var storageParts = source.StorageParts.Where(rec => rec.PartId == setPart.PartId);
                foreach (var storagePart in storageParts)
                {
                    // компонентов на одном слкаде может не хватать     
                    if (storagePart.Count >= countOnStorages)
                    {
                        storagePart.Count -= countOnStorages;
                        break;
                    }
                    else
                    {
                        countOnStorages -= storagePart.Count;
                        storagePart.Count = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.Status = ProcedureStatus.Выполняется;
        } 

            public void FinishProcedure(ProcedureBindingModel model) {
            Procedure element = source.Procedures.FirstOrDefault(rec => rec.Id == model.Id); if (element == null)
                { throw new Exception("Элемент не найден"); } if (element.Status != ProcedureStatus.Выполняется)
            { throw new Exception("Заказ не в статусе \"Выполняется\""); } element.Status = ProcedureStatus.Готов; }

            public void PayProcedure(ProcedureBindingModel model)
            {
            Procedure element = source.Procedures.FirstOrDefault(rec => rec.Id == model.Id); if (element == null) { throw new Exception("Элемент не найден"); }
                if (element.Status != ProcedureStatus.Готов)
                {
                    throw new Exception("Заказ не в статусе \"Готов\"");
            }
                element.Status = ProcedureStatus.Оплачен;
            }

            public void PutPartOnStorage(StoragePartBindingModel model) { StoragePart element = source.StorageParts.FirstOrDefault(rec => rec.StorageId == model.StorageId && rec.PartId == model.PartId); if (element != null) { element.Count += model.Count; } else { int maxId = source.StorageParts.Count > 0 ? source.StorageParts.Max(rec => rec.Id) : 0;
                source.StorageParts.Add(new StoragePart { Id = ++maxId, StorageId = model.StorageId, PartId = model.PartId, Count = model.Count }); } }
        }
}
