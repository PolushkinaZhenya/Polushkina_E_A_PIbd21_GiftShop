using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopModel
{
    /// <summary>     
    ///  Изделие, изготавливаемое в магазине    
    ///  </summary>  
    public class Set
    {
        public int Id { get; set; }
        public string SetName { get; set; }
        public decimal Price { get; set; }
    }
}
