using System;
using System.Windows.Forms;
using RepairContracts.BusinessLogicsContracts;
using RepairContracts.StorageContracts;
using RepairBusinessLogic.BusinessLogics;
using RepairDatabaseImplement.Implements;
using RepairBusinessLogic.OfficePackage.Implements;
using RepairBusinessLogic.OfficePackage;
using System.Configuration;
using System.Threading;
using Unity;
using Unity.Lifetime;
using RepairBusinessLogic.MailWorker;
using RepairContracts.BindingModels;

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
            var mailSender = Container.Resolve<AbstractMailWorker>();
            mailSender.MailConfig(new MailConfigBindingModel
            {
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = ConfigurationManager.AppSettings["MailPassword"],
                SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                PopHost = ConfigurationManager.AppSettings["PopHost"],
                PopPort = Convert.ToInt32(ConfigurationManager.AppSettings["PopPort"])
            });
            // ������� ������
            var timer = new System.Threading.Timer(new TimerCallback(MailCheck), null, 0, 100000);
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

            currentContainer.RegisterType<IClientStorage, ClientStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IClientLogic, ClientLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IImplementerStorage, ImplementerStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IMessageInfoStorage, MessageInfoStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IImplementerLogic, ImplementerLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IComponentLogic, ComponentLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IRepairLogic, RepairLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IReportLogic, ReportLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IMessageInfoLogic, MessageInfoLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IWorkProcess, WorkModeling>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<AbstractMailWorker, MailKitWorker>(new SingletonLifetimeManager());

            return currentContainer;
        }
        private static void MailCheck(object obj) =>Container.Resolve<AbstractMailWorker>().MailCheck();
    }
}


