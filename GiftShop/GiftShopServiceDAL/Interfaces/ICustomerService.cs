﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;

namespace GiftShopServiceDAL.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetList();
        CustomerViewModel GetElement(int id);
        void AddElement(СustomerBindingModel model);
        void UpdElement(СustomerBindingModel model);
        void DelElement(int id);
    }
}
