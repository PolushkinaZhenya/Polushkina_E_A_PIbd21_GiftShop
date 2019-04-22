using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace GiftShopServiceDAL.ViewModel
{

    public class StorageViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название склада")]
        public string StorageName { get; set; }

        public List<StoragePartViewModel> StorageParts { get; set; }
    }
}
