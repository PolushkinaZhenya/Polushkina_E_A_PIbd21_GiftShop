using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
namespace GiftShopServiceDAL.Interfaces
{
    public interface IRecordService
    {
        void SaveSetPrice(RecordBindingModel model);

        List<StoragesLoadViewModel> GetStoragesLoad();

        void SaveStoragesLoad(RecordBindingModel model);

        List<CustomerProceduresModel> GetCustomerProcedures(RecordBindingModel model);

        void SaveCustomerProcedures(RecordBindingModel model);
    }
}
