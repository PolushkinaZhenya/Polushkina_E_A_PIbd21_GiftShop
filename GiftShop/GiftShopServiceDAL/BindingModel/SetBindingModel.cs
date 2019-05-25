using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GiftShopServiceDAL.BindingModel
{
    [DataContract]
    public class SetBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string SetName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<SetPartBindingModel> SetParts { get; set; }
    }
}
