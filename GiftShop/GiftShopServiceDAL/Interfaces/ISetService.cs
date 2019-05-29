using System.Collections.Generic;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceDAL.Attributies;

namespace GiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с товарами")]
    public interface ISetService
    {
        [CustomMethod("Метод получения списка товаров")]
        List<SetViewModel> GetList();

        [CustomMethod("Метод получения товара по id")]
        SetViewModel GetElement(int id);

        [CustomMethod("Метод добавления товара")]
        void AddElement(SetBindingModel model);

        [CustomMethod("Метод изменения данных по товару")]
        void UpdElement(SetBindingModel model);

        [CustomMethod("Метод удаления товару")]
        void DelElement(int id);
    }
}
