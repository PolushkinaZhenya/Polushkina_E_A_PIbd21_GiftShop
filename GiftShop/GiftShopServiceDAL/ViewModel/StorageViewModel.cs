using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace GiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class StorageViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название склада")]
        public string StorageName { get; set; }

        [DataMember]
        public List<StoragePartViewModel> StorageParts { get; set; }
    }
}
