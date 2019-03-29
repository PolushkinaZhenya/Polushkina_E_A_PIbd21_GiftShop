using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GiftShopModel
{
    /// <summary>     
    ///  Изделие, изготавливаемое в магазине    
    ///  </summary>  
    public class Set
    {
        public int Id { get; set; }
        [Required]
        public string SetName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual List<Procedure> Procedures { get; set; }
    }
}
