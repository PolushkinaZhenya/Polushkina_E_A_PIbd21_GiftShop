using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopServiceDAL.BindingModel
{
    public class SetBindingModel
    {
        public int Id { get; set; }
        public string SetName { get; set; }
        public decimal Price { get; set; }
        public List<SetPartBindingModel> SetParts { get; set; }
    }
}
