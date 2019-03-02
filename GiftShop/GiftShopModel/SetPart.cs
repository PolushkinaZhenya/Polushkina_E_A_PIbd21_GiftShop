using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopModel
{
    /// <summary>    
    /// Сколько компонентов, требуется при изготовлении изделия    
    /// </summary>    
    public class SetPart
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; }
        public int Count { get; set; }
    }
}
