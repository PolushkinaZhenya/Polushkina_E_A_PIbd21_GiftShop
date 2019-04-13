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
    public class ProcedureViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }

        [DataMember]
        public int SetId { get; set; }

        [DataMember]
        [DisplayName("Продукт")]
        public string SetName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
        
        [DataMember]
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DataMember]
        [DisplayName("Статус")]
        public string Status { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }

        [DataMember]
        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }
    }
}
