using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GiftShopModel
{
    /// <summary>    
    /// Компонент, требуемый для изготовления изделия    
    /// </summary>    
    public class Part
    {
        public int Id { get; set; }
        [Required]
        public string PartName { get; set; }
        public virtual List<SetPart> SetParts { get; set; }
        public virtual List<StoragePart> StorageParts { get; set; }
    }
}
