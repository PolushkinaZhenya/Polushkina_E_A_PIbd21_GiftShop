using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace GiftShopServiceDAL.Interfaces
{
    public interface ISellerService
    {
        List<SellerViewModel> GetList();

        SellerViewModel GetElement(int id);

        void AddElement(SellerBindingModel model);

        void UpdElement(SellerBindingModel model);

        void DelElement(int id);

        SellerViewModel GetFreeSeller();
    }
}
