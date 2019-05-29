using System.Collections.Generic;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceDAL.Attributies;

namespace GiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<ProcedureViewModel> GetList();

        [CustomMethod("Метод получения списка свободных заказов")]
        List<ProcedureViewModel> GetFreeProcedures();

        [CustomMethod("Метод создания заказа")]
        void CreateProcedure(ProcedureBindingModel model);

        [CustomMethod("Метод принятия заказа в работу")]
        void TakeProcedureInWork(ProcedureBindingModel model);

        [CustomMethod("Метод завершения заказа")]
        void FinishProcedure(ProcedureBindingModel model);

        [CustomMethod("Метод оплаты заказа")]
        void PayProcedure(ProcedureBindingModel model);

        [CustomMethod("Метод добавления компонента на склад")]
        void PutPartOnStorage(StoragePartBindingModel model);
    }
}
