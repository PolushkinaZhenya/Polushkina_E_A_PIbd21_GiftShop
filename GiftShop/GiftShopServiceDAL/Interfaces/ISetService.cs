﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.ViewModel;
namespace GiftShopServiceDAL.Interfaces
{
    public interface ISetService
    {
        List<SetViewModel> GetList();
        SetViewModel GetElement(int id);
        void AddElement(SetBindingModel model);
        void UpdElement(SetBindingModel model);
        void DelElement(int id);
    }
}
