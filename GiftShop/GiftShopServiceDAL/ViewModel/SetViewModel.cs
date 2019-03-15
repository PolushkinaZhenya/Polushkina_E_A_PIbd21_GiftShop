using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GiftShopServiceDAL.ViewModel
{
    public class SetViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название продукта")]
        public string SetName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public List<SetPartViewModel> SetParts { get; set; }
    }
}
