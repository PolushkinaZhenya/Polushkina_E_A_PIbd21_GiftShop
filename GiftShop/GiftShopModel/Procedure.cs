using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopModel
{
    /// <summary>     
    /// Заказ клиента     
    /// </summary>     
    public class Procedure
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SetId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public ProcedureStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; } //DateTime?- можно хратить значение null
        public virtual Customer Customer { get; set; }
        public virtual Set Set { get; set; }
    }
}
