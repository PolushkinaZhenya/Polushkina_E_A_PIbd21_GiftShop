using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopModel;
namespace GiftShopServiceImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Сustomer> Costomers { get; set; }
        public List<Part> Parts { get; set; }

        public List<Procedure> Procedures { get; set; }

        public List<Set> Sets { get; set; }

        public List<SetPart> SetParts { get; set; }

        private DataListSingleton() {
            Costomers = new List<Сustomer>();
            Parts = new List<Part>();
            Procedures = new List<Procedure>();
            Sets = new List<Set>();
            SetParts = new List<SetPart>();
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
