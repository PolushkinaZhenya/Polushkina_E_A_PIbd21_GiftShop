using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopServiceDAL.BindingModel
{
    public class ProcedureBindingModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SetId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
