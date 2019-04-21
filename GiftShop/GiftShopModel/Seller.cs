using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftShopModel
{  
    /// <summary>  
   /// Продавец, выполняющий заказы клиентов 
   /// /// </summary> 
    public class Seller
    {
        public int Id { get; set; }
        [Required]
        public string SellerFIO { get; set; }
        [ForeignKey("SellerId")]
        public virtual List<Procedure> Procedures { get; set; }
    }
}
