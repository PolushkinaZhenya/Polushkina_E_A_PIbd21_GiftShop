using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopModel;
using System.Data.Entity;
namespace GiftShopServiceImplementDataBase
{
    public class GiftDbContext : DbContext
    {
        public GiftDbContext() : base("GiftDatabase")
        {     
            //настройки конфигурации для entity  
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        } 
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Part> Parts { get; set; }

        public virtual DbSet<Procedure> Procedures { get; set; }

        public virtual DbSet<Set> Sets { get; set; }

        public virtual DbSet<Seller> Sellers { get; set; }

        public virtual DbSet<SetPart> SetParts { get; set; }

        public virtual DbSet<Storage> Storages { get; set; }

        public virtual DbSet<StoragePart> StorageParts { get; set; }

        public virtual DbSet<MessageInfo> MessageInfos { get; set; }
    }
}
