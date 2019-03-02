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
        public MainServiceList() {
            source = DataListSingleton.GetInstance();
        }
        public List<ProcedureViewModel> GetList()
        {
            List<ProcedureViewModel> result = new List<ProcedureViewModel>();
            for (int i = 0; i < source.Procedures.Count; ++i)
            {
                string costomerFIO = string.Empty;
                for (int j = 0; j < source.Customers.Count; ++j)
                {
                    if (source.Customers[j].Id == source.Procedures[i].CustomerId)
                    {
                        costomerFIO = source.Customers[j].CustomerFIO;
                        break;
                    }
                }
                string setName = string.Empty;
                for (int j = 0; j < source.Sets.Count; ++j)
                {
                    if (source.Sets[j].Id == source.Procedures[i].SetId)
                    {
                        setName = source.Sets[j].SetName;
                        break;
                    }
                }
                result.Add(new ProcedureViewModel
                {
                    Id = source.Procedures[i].Id,
                    CustomerId = source.Procedures[i].CustomerId,
                    CostomerFIO = costomerFIO,
                    SetId = source.Procedures[i].SetId,
                    SetName = setName,
                    Count = source.Procedures[i].Count,
                    Sum = source.Procedures[i].Sum,
                    DateCreate = source.Procedures[i].DateCreate.ToLongDateString(),
                    DateImplement = source.Procedures[i].DateImplement?.ToLongDateString(),
                    Status = source.Procedures[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateProcedure(ProcedureBindingModel model)
        {
            int maxId = 0; for (int i = 0; i < source.Procedures.Count; ++i)
            {
                if (source.Procedures[i].Id > maxId)
                {
                    maxId = source.Customers[i].Id;
                }
            }
            source.Procedures.Add(new Procedure
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                SetId = model.SetId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = ProcedureStatus.Принят
            });
        }

        public void TakeProcedureInWork(ProcedureBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Procedures.Count; ++i)
            {
                if (source.Procedures[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Procedures[index].Status != ProcedureStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.Procedures[index].DateImplement = DateTime.Now;
            source.Procedures[index].Status = ProcedureStatus.Выполняется;
        }

        public void FinishProcedure(ProcedureBindingModel model)
        {
            int index = -1; for (int i = 0; i < source.Procedures.Count; ++i)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Procedures[index].Status != ProcedureStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.Procedures[index].Status = ProcedureStatus.Готов;
        }
        public void PayProcedure(ProcedureBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Procedures.Count; ++i)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Procedures[index].Status != ProcedureStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.Procedures[index].Status = ProcedureStatus.Оплачен;
        }
    }
}
