using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GiftShopModel
{
    /// <summary>
    /// /// Хранилиище компонентов в магазине 
    /// /// </summary>
    public class Storage
    {
        public int Id { get; set; }
        [Required]
        public string StorageName { get; set; }
        public virtual List<StoragePart> StorageParts { get; set; }
    }
}
