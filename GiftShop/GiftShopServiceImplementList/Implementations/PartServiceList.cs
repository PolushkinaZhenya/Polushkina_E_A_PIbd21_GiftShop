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
    public class PartServiceList : IPartService
    {
        private DataListSingleton source;
        public PartServiceList()
        {
            source = DataListSingleton.GetInstance();
        }  
        public List<PartViewModel> GetList()
        {
            List<PartViewModel> result = new List<PartViewModel>();
            for (int i = 0; i < source.Parts.Count; ++i)
            {
                result.Add(new PartViewModel
                {
                    Id = source.Parts[i].Id,
                    PartName = source.Parts[i].PartName
                });
            }
            return result;
        }

        public PartViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Parts.Count; ++i)
            {
                if (source.Parts[i].Id == id)
                {
                    return new PartViewModel
                    {
                        Id = source.Parts[i].Id,
                        PartName = source.Parts[i].PartName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(PartBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Parts.Count; ++i)
            {
                if (source.Parts[i].Id > maxId)
                {
                    maxId = source.Parts[i].Id;
                }
                if (source.Parts[i].PartName == model.PartName)
                {
                    throw new Exception("Уже есть такой компонент");
                }
            }
            source.Parts.Add(new Part
            {
                Id = maxId + 1,
                PartName = model.PartName
            });
        }

        public void UpdElement(PartBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Parts.Count; ++i)
            {
                if (source.Parts[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Parts[i].PartName == model.PartName && source.Parts[i].Id != model.Id)
                {
                    throw new Exception("Уже есть такой компонент");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Parts[index].PartName = model.PartName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Parts.Count; ++i)
            {
                if (source.Parts[i].Id == id)
                {
                    source.Parts.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

    }
}
