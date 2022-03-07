using System;
using System.Windows.Forms;
using RepairContracts.BusinessLogicsContracts;
using RepairContracts.StorageContracts;
using RepairBusinessLogic.BusinessLogics;
using RepairDatabaseImplement.Implements;
using Unity;
using Unity.Lifetime;

namespace RepairView
{
    static class Program
    {

        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<IComponentStorage, ComponentStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new  HierarchicalLifetimeManager());

            currentContainer.RegisterType<IRepairStorage, RepairStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IComponentLogic, ComponentLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IRepairLogic, RepairLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}

