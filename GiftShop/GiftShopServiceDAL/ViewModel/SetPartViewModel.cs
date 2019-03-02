using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace GiftShopServiceDAL.ViewModel
{
    public class SetPartViewModel
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int PartId { get; set; }
        [DisplayName("Компонент")]
        public string PartName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
