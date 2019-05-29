using System.Collections.Generic;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceDAL.Attributies;

namespace GiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IRecordService
    {
        [CustomMethod("Метод сохранения отчета по стоимости товаров")]
        void SaveSetPrice(RecordBindingModel model);

        [CustomMethod("Метод получения списка загруженности складов")]
        List<StoragesLoadViewModel> GetStoragesLoad();

        [CustomMethod("Метод сохранения отчета по загруженности складов")]
        void SaveStoragesLoad(RecordBindingModel model);

        [CustomMethod("Метод получения списка заказов")]
        List<CustomerProceduresModel> GetCustomerProcedures(RecordBindingModel model);

        [CustomMethod("Метод сохранения отчета по заказам")]
        void SaveCustomerProcedures(RecordBindingModel model);
    }
}
