using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace GiftShopServiceDAL.ViewModel
{
    public class ProcedureViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }

        public int SetId { get; set; }
        [DisplayName("Продукт")]
        public string SetName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }
    }
}
