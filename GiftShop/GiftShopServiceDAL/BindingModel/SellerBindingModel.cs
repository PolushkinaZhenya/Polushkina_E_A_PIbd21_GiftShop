using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GiftShopServiceDAL.BindingModel
{
    [DataContract]
    public class SellerBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string SellerFIO { get; set; }
    }
}
