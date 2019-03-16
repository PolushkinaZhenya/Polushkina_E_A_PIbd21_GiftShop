using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
namespace GiftShopServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<ProcedureViewModel> GetList();

        void CreateProcedure(ProcedureBindingModel model);

        void TakeProcedureInWork(ProcedureBindingModel model);

        void FinishProcedure(ProcedureBindingModel model);

        void PayProcedure(ProcedureBindingModel model);

        void PutPartOnStorage(StoragePartBindingModel model);
    }
}
