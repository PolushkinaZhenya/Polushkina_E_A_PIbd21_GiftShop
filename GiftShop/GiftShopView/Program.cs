﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceImplementList.Implementations;
using GiftShopServiceImplementDataBase;
using GiftShopServiceImplementDataBase.Implementations;
using System.Data.Entity;

namespace GiftShopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            APIClient.Connect();
            MailClient.Connect();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
