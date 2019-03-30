using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceImplementList.Implementations;
using Unity;
using Unity.Lifetime;
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
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext,
                GiftDbContext>(new 
                HierarchicalLifetimeManager());
                currentContainer.RegisterType<ICustomerService, CustomerServiceDB>(new 
                    HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPartService, PartServiceDB>(new 
                HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISetService, SetServiceDB>(new 
                HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceDB>(new 
                HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new 
                HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRecordService, RecordServiceDB>(new
                HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
