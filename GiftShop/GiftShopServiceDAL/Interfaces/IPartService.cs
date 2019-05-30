using System.Collections.Generic;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceDAL.Attributies;

namespace GiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с компонентами")]
    public interface IPartService
    {
        [CustomMethod("Метод получения списка компонентов")]
        List<PartViewModel> GetList();

        [CustomMethod("Метод получения компонента по id")]
        PartViewModel GetElement(int id);

        [CustomMethod("Метод добавления компонента")]
        void AddElement(PartBindingModel model);

        [CustomMethod("Метод изменения данных по компоненту")]
        void UpdElement(PartBindingModel model);

        [CustomMethod("Метод удаления компенента")]
        void DelElement(int id);
    }
}
