using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GiftShopServiceDAL.ViewModel
{
    public class PartViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string PartName { get; set; }
    }
}
