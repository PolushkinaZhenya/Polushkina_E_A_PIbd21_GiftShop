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
    public class SellerServiceDB : ISellerService
    {
        private GiftDbContext context;

        public SellerServiceDB(GiftDbContext context)
        {
            this.context = context;
        }
        public List<SellerViewModel> GetList()
        {
            List<SellerViewModel> result = context.Sellers
            .Select(rec => new SellerViewModel
            {
                Id = rec.Id,
                SellerFIO = rec.SellerFIO
            })
            .ToList();
            return result;
        }
        public SellerViewModel GetElement(int id)
        {
            Seller element = context.Sellers.FirstOrDefault(rec => rec.Id ==
            id);
            if (element != null)
            {
                return new SellerViewModel
                {
                    Id = element.Id,
                    SellerFIO = element.SellerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SellerBindingModel model)
        {
            Seller element = context.Sellers.FirstOrDefault(rec =>
            rec.SellerFIO == model.SellerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть продавец с таким ФИО");
            }
            context.Sellers.Add(new Seller
            {
                SellerFIO = model.SellerFIO
            });
            context.SaveChanges();
        }
        public void UpdElement(SellerBindingModel model)
        {
            Seller element = context.Sellers.FirstOrDefault(rec =>
            rec.SellerFIO == model.SellerFIO &&
            rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть продавец с таким ФИО");
            }
            element = context.Sellers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SellerFIO = model.SellerFIO;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Seller element = context.Sellers.FirstOrDefault(rec => rec.Id ==
            id);
            if (element != null)
            {
                context.Sellers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public SellerViewModel GetFreeSeller()
        {
            var ordersWorker = context.Sellers
                .Select(x => new
                {
                    ImplId = x.Id,
                    Count = context.Procedures.Where(o => o.Status == ProcedureStatus.Выполняется && o.SellerId == x.Id).Count()
                })
                .OrderBy(x => x.Count)
                .FirstOrDefault();
            if (ordersWorker != null)
            {
                return GetElement(ordersWorker.ImplId);
            }
            return null;
        }
    }
}
