﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;

namespace GiftShopServiceDAL.Interfaces
{
    public interface IMessageInfoService
    {
        List<MessageInfoViewModel> GetList();

        MessageInfoViewModel GetElement(int id);

        void AddElement(MessageInfoBindingModel model);
    }
}
