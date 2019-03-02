using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
namespace GiftShopServiceDAL.Interfaces
{
    public interface ISetPartService
    {
        List<SetPartViewModel> GetList();

        SetPartViewModel GetElement(int id);

        void AddElement(SetPartBindingModel model);

        void UpdElement(SetPartBindingModel model);

        void DelElement(int id);
    }
}
