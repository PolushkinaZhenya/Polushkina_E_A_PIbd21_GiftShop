using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopModel;
namespace GiftShopServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Customer> Customers { get; set; }
        public List<Part> Parts { get; set; }
        public List<Procedure> Procedures { get; set; }
        public List<Set> Sets { get; set; }
        public List<SetPart> SetParts { get; set; }
        public List<Storage> Storages { get; set; }

        public List<StoragePart> StorageParts { get; set; }
        private DataListSingleton()
        {
            Customers = new List<Customer>();
            Parts = new List<Part>();
            Procedures = new List<Procedure>();
            Sets = new List<Set>();
            SetParts = new List<SetPart>();
            Storages = new List<Storage>();
            StorageParts = new List<StoragePart>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
