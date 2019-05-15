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
        public SetServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SetViewModel> GetList()
        {
            List<SetViewModel> result = new List<SetViewModel>();
            for (int i = 0; i < source.Sets.Count; ++i)
            {                 // требуется дополнительно получить список компонентов для изделия и их количество    
                List<SetPartViewModel> setParts = new List<SetPartViewModel>();
                for (int j = 0; j < source.SetParts.Count; ++j)
                {
                    if (source.SetParts[j].SetId == source.Sets[i].Id)
                    {
                        string partName = string.Empty;
                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.SetParts[j].PartId == source.Parts[k].Id)
                            {
                                partName = source.Parts[k].PartName;
                                break;
                            }
                        }
                        setParts.Add(new SetPartViewModel
                        {
                            Id = source.SetParts[j].Id,
                            SetId = source.SetParts[j].SetId,
                            PartId = source.SetParts[j].PartId,
                            PartName = partName,
                            Count = source.SetParts[j].Count
                        });
                    }
                }
                result.Add(new SetViewModel
                {
                    Id = source.Sets[i].Id,
                    SetName = source.Sets[i].SetName,
                    Price = source.Sets[i].Price,
                    SetParts = setParts
                });
            }
            return result;
        }

        public SetViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Sets.Count; ++i)
            {                 // требуется дополнительно получить список компонентов для изделия и их количество                 
                List<SetPartViewModel> setParts = new List<SetPartViewModel>();
                for (int j = 0; j < source.SetParts.Count; ++j)
                {
                    if (source.SetParts[j].SetId == source.Sets[i].Id)
                    {
                        string partName = string.Empty;
                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.SetParts[j].PartId == source.Parts[k].Id)
                            {
                                partName = source.Parts[k].PartName;
                                break;
                            }
                        }
                        setParts.Add(new SetPartViewModel
                        {
                            Id = source.SetParts[j].Id,
                            SetId = source.SetParts[j].SetId,
                            PartId = source.SetParts[j].PartId,
                            PartName = partName,
                            Count = source.SetParts[j].Count
                        });
                    }
                }
                if (source.Sets[i].Id == id)
                {
                    return new SetViewModel
                    {
                        Id = source.Sets[i].Id,
                        SetName = source.Sets[i].SetName,
                        Price = source.Sets[i].Price,
                        SetParts = setParts
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(SetBindingModel model)
        {
            int maxId = 0; for (int i = 0; i < source.Sets.Count; ++i)
            {
                if (source.Sets[i].Id > maxId)
                {
                    maxId = source.Sets[i].Id;
                }
                if (source.Sets[i].SetName == model.SetName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Sets.Add(new Set
            {
                Id = maxId + 1,
                SetName = model.SetName,
                Price = model.Price
            }); // компоненты для изделия      
            int maxPCId = 0; for (int i = 0; i < source.SetParts.Count; ++i)
            {
                if (source.SetParts[i].Id > maxPCId)
                {
                    maxPCId = source.SetParts[i].Id;
                }
            }             // убираем дубли по компонентам   
            for (int i = 0; i < model.SetParts.Count; ++i)
            {
                for (int j = 1; j < model.SetParts.Count; ++j)
                {
                    if (model.SetParts[i].PartId == model.SetParts[j].PartId)
                    {
                        model.SetParts[i].Count += model.SetParts[j].Count;
                        model.SetParts.RemoveAt(j--);
                    }
                }
            }             // добавляем компоненты     
            for (int i = 0; i < model.SetParts.Count; ++i)
            {
                source.SetParts.Add(new SetPart
                {
                    Id = ++maxPCId,
                    SetId = maxId + 1,
                    PartId = model.SetParts[i].PartId,
                    Count = model.SetParts[i].Count
                });
            }
        }

        public void UpdElement(SetBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Sets.Count; ++i)
            {
                if (source.Sets[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Sets[i].SetName == model.SetName && source.Sets[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Sets[index].SetName = model.SetName;
            source.Sets[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.SetParts.Count; ++i)
            {
                if (source.SetParts[i].Id > maxPCId) { maxPCId = source.SetParts[i].Id; }
            }             // обновляем существуюущие компоненты       
            for (int i = 0; i < source.SetParts.Count; ++i)
            {
                if (source.SetParts[i].SetId == model.Id)
                {
                    bool flag = true; for (int j = 0; j < model.SetParts.Count; ++j)
                    {                         // если встретили, то изменяем количество      
                        if (source.SetParts[i].Id == model.SetParts[j].Id)
                        {
                            source.SetParts[i].Count = model.SetParts[j].Count;
                            flag = false;
                            break;
                        }
                    }                     // если не встретили, то удаляем              
                    if (flag) {
                        source.SetParts.RemoveAt(i--);
                    }
                }
            }             // новые записи             
            for (int i = 0; i < model.SetParts.Count; ++i)
            {
                if (model.SetParts[i].Id == 0)
                {                     // ищем дубли         
                    for (int j = 0; j < source.SetParts.Count; ++j)
                    {
                        if (source.SetParts[j].SetId == model.Id && source.SetParts[j].PartId == model.SetParts[i].PartId)
                        {
                            source.SetParts[j].Count += model.SetParts[i].Count;
                            model.SetParts[i].Id = source.SetParts[j].Id;
                            break;
                        }
                    }                     // если не нашли дубли, то новая запись  
                    if (model.SetParts[i].Id == 0)
                    {
                        source.SetParts.Add(new SetPart
                        {
                            Id = ++maxPCId,
                            SetId = model.Id,
                            PartId = model.SetParts[i].PartId,
                            Count = model.SetParts[i].Count
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {             // удаяем записи по компонентам при удалении изделия      
            for (int i = 0; i < source.SetParts.Count; ++i)
            {
                if (source.SetParts[i].SetId == id)
                {
                    source.SetParts.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Sets.Count; ++i)
            {
                if (source.Sets[i].Id == id)
                {
                    source.Sets.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
