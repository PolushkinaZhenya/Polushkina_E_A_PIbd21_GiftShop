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
    public class PartServiceDB : IPartService
    {
        private GiftWebDbContext context;

        public PartServiceDB(GiftWebDbContext context)
        {
            this.context = context;
        }

        public PartServiceDB()
        {
            this.context = new GiftWebDbContext();
        }

        public List<PartViewModel> GetList()
        {
            List<PartViewModel> result = context.Parts.Select(rec => new
            PartViewModel
            {
                Id = rec.Id,
                PartName = rec.PartName
            })
            .ToList();
            return result;
        }
        public PartViewModel GetElement(int id)
        {
            Part part = context.Parts.FirstOrDefault(rec => rec.Id == id);
            if (part != null)
            {
                return new PartViewModel
                {
                    Id = part.Id,
                    PartName = part.PartName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(PartBindingModel model)
        {
            Part part = context.Parts.FirstOrDefault(rec => rec.PartName ==
            model.PartName);
            if (part != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            context.Parts.Add(new Part
            {
                PartName = model.PartName
            });
            context.SaveChanges();
        }
        public void UpdElement(PartBindingModel model)
        {
            Part part = context.Parts.FirstOrDefault(rec => rec.PartName ==
            model.PartName && rec.Id != model.Id);
            if (part != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            part = context.Parts.FirstOrDefault(rec => rec.Id == model.Id);
            if (part == null)
            {
                throw new Exception("Элемент не найден");
            }
            part.PartName = model.PartName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Part part = context.Parts.FirstOrDefault(rec => rec.Id == id);
            if (part != null)
            {
                context.Parts.Remove(part);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
