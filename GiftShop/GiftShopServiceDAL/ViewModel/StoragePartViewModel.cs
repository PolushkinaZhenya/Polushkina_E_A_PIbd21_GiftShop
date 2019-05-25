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
    public class StoragePartViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StorageId { get; set; }

        [DataMember]
        public int PartId { get; set; }

        [DataMember]
        [DisplayName("Название компонента")]
        public string PartName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
