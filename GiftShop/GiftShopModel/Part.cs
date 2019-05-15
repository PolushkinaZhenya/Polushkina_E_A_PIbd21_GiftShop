using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopModel
{
    /// <summary>    
    /// Компонент, требуемый для изготовления изделия    
    /// </summary>    
    public class Part
    {
        public int Id { get; set; }
        public string PartName { get; set; }
    }
}
