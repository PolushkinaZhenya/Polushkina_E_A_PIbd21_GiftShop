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
    public class SetServiceList : ISetService
    {
        private DataListSingleton source;

        public SetServiceList() { source = DataListSingleton.GetInstance(); }

        public List<SetViewModel> GetList()
        {
            List<SetViewModel> result = source.Sets.Select(rec => new SetViewModel
            {
                Id = rec.Id,
                SetName = rec.SetName,
                Price = rec.Price,
                SetParts = source.SetParts.Where(recPC => recPC.SetId == rec.Id).Select(recPC => new SetPartViewModel
                {
                    Id = recPC.Id,
                    SetId = recPC.SetId,
                    PartId = recPC.PartId,
                    PartName = source.Parts.FirstOrDefault(recC => recC.Id == recPC.PartId)?.PartName,
                    Count = recPC.Count
                }).ToList()
            }).ToList();

            return result;
        }

        public SetViewModel GetElement(int id)
        {
            Set element = source.Sets.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SetViewModel
                {
                    Id = element.Id,
                    SetName = element.SetName,
                    Price = element.Price,
                    SetParts = source.SetParts.Where(recPC => recPC.SetId == element.Id).Select(recPC => new SetPartViewModel
                    {
                        Id = recPC.Id,
                        SetId = recPC.SetId,
                        PartId = recPC.PartId,
                        PartName = source.Parts.FirstOrDefault(recC => recC.Id == recPC.PartId)?.PartName,
                        Count = recPC.Count
                    }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SetBindingModel model)
        {
            Set element = source.Sets.FirstOrDefault(rec => rec.SetName == model.SetName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Sets.Count > 0 ? source.Sets.Max(rec => rec.Id) : 0; source.Sets.Add(new Set
            {
                Id = maxId + 1,
                SetName = model.SetName,
                Price = model.Price
            });             // компоненты для изделия        
            int maxPCId = source.SetParts.Count > 0 ? source.SetParts.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам     
            var groupParts = model.SetParts.GroupBy(rec => rec.PartId)
                .Select(rec => new
                {
                    PartId = rec.Key,
                    Count = rec.Sum(r => r.Count)
                });
            // добавляем компоненты 
            foreach (var groupComponent in groupParts)
            {
                source.SetParts.Add(new SetPart
                {
                    Id = ++maxPCId,
                    SetId = maxId + 1,
                    PartId = groupComponent.PartId,
                    Count = groupComponent.Count
                });
            }
        }

        public void UpdElement(SetBindingModel model)
        {
            Set element = source.Sets.FirstOrDefault(rec => rec.SetName == model.SetName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Sets.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SetName = model.SetName; element.Price = model.Price;
            int maxPCId = source.SetParts.Count > 0 ? source.SetParts.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты  
            var compIds = model.SetParts.Select(rec => rec.PartId).Distinct();
            var updateComponents = source.SetParts.Where(rec =>
            rec.SetId == model.Id && compIds.Contains(rec.SetId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count = model.SetParts.FirstOrDefault(rec =>
                rec.Id == updateComponent.Id).Count;
            }
            source.SetParts.RemoveAll(rec => rec.SetId == model.Id && !compIds.Contains(rec.PartId));
            // новые записи 
            var groupParts = model.SetParts.Where(rec => rec.Id == 0)
                .GroupBy(rec => rec.PartId)
                .Select(rec => new
                {
                    PartId = rec.Key,
                    Count = rec.Sum(r => r.Count)
                });
            foreach (var groupPart in groupParts)
            {
                SetPart elementPC = source.SetParts.FirstOrDefault(rec => rec.SetId == model.Id && rec.PartId == groupPart.PartId);
                if (elementPC != null)
                {
                    elementPC.Count += groupPart.Count;
                }
                else
                {
                    source.SetParts.Add(new SetPart
                    {
                        Id = ++maxPCId,
                        SetId = model.Id,
                        PartId = groupPart.PartId,
                        Count = groupPart.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Set element = source.Sets.FirstOrDefault(rec => rec.Id == id); if (element != null)
            {                 // удаяем записи по компонентам при удалении изделия  
                source.SetParts.RemoveAll(rec => rec.SetId == id);
                source.Sets.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
