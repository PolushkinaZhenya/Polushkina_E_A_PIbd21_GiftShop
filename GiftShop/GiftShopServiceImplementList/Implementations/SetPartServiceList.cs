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
    public class SetPartServiceList: ISetPartService
    {
        private DataListSingleton source;

        public SetPartServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<SetPartViewModel> GetList()
        {
            List<SetPartViewModel> result = new List<SetPartViewModel>();
            for (int i = 0; i < source.SetParts.Count; ++i)
            {
                result.Add(new SetPartViewModel
                {
                    Id = source.SetParts[i].Id,
                    SetId = source.SetParts[i].SetId,
                    PartId = source.SetParts[i].PartId,
                    Count = source.SetParts[i].Count,
                    PartName = source.SetParts[i].PartName
                });
            }
            return result;
        }

        public SetPartViewModel GetElement(int id)
        {
            for (int i = 0; i < source.SetParts.Count; ++i)
            {
                if (source.SetParts[i].Id == id)
                {
                    return new SetPartViewModel
                    {
                        Id = source.SetParts[i].Id,
                        PartName = source.SetParts[i].PartName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SetPartBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.SetParts.Count; ++i)
            {
                if (source.SetParts[i].Id > maxId)
                {
                    maxId = source.SetParts[i].Id;
                }
                if (source.SetParts[i].PartName == model.PartName)
                {
                    throw new Exception("Уже есть ингредиент с таким именем");
                }
            }
            source.SetParts.Add(new SetPart
            {
                Id = maxId + 1,
                SetId = model.SetId,
                PartId = model.PartId,
                Count = model.Count,
                PartName = model.PartName
            });
        }

        public void UpdElement(SetPartBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.SetParts.Count; ++i)
            {
                if (source.SetParts[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.SetParts[i].PartName == model.PartName &&
                source.SetParts[i].Id != model.Id)
                {
                    throw new Exception("Уже есть ингредиент с таким именем");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.SetParts[index].PartName = model.PartName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.SetParts.Count; ++i)
            {
                if (source.SetParts[i].Id == id)
                {
                    source.SetParts.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
