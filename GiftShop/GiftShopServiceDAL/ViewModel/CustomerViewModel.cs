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
    public class CustomerViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }
    }
}
