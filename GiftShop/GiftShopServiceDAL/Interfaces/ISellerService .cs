using System.Collections.Generic;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceDAL.Attributies;

namespace GiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с продавцами")]
    public interface ISellerService
    {
        [CustomMethod("Метод получения списка продавцов")]
        List<SellerViewModel> GetList();

        [CustomMethod("Метод получения продавца по id")]
        SellerViewModel GetElement(int id);

        [CustomMethod("Метод добавления продавца")]
        void AddElement(SellerBindingModel model);

        [CustomMethod("Метод изменения данных по продавцу")]
        void UpdElement(SellerBindingModel model);

        [CustomMethod("Метод удаления продавца")]
        void DelElement(int id);

        [CustomMethod("Метод получения свободного продавца")]
        SellerViewModel GetFreeSeller();
    }
}
