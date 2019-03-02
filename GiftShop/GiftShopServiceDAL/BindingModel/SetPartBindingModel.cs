using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopServiceDAL.BindingModel
{
    public class SetPartBindingModel
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; }
        public int Count { get; set; }
    }
}
