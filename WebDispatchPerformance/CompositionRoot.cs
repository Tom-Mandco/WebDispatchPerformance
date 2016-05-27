using System;
using System.Configuration;
using Ninject.Modules;
using NLog;
using MCO.Applications.WebDispatchPerformance;
using MCO.Applications.WebDispatchPerformance.Classes;
using MCO.Applications.WebDispatchPerformance.Interfaces;
using MCO.Data.WebDispatchPerformance;
using MCO.Data.WebDispatchPerformance.Models;
using MCO.Data.WebDispatchPerformance.Interfaces;
using MCO.Services.WebDispatchPerformance;
using MCO.Services.WebDispatchPerformance.Interfaces;

namespace MCO.Applications.WebDispatchPerformance
{
    public class CompositionRoot : NinjectModule
    {
        public override void Load()
        {
            string connectionString = "";
            try
            {
                connectionString = ConfigurationManager.AppSettings["OracleConnection"];
            }
            catch (Exception ex)
            {

            }

            Bind<IRepository>().To<OracleRepository>().InSingletonScope().WithConstructorArgument("connectionString", connectionString);
            Bind<IPerformLookup>().To<PerformLookup>();
            Bind<ILog>().ToMethod(x =>
            {
                var scope = x.Request.ParentRequest.Service.FullName;
                var log = (ILog)LogManager.GetLogger(scope, typeof(Log));
                return log;
            });

            Bind<IApp>().To<App>();
            Bind<IDataHander>().To<DataHandler>();
            Bind<IExcelHandler>().To<ExcelHandler>();
            Bind<IExcelWriter>().To<ExcelWriter>();

            Bind<DispatchDetails>().ToSelf();

        }
    }
}
