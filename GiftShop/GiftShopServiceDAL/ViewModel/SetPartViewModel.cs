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
    public class SetPartViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int SetId { get; set; }

        [DataMember]
        public int PartId { get; set; }

        [DataMember]
        [DisplayName("Компонент")]
        public string PartName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
