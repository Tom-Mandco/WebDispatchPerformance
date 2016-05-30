namespace MCO.Applications.WebDispatchPerformance
{
    using Ninject.Modules;
    using NLog;
    using Interfaces;
    using Classes;
    using MCO.Data.WebDispatchPerformance.Models;
    using MCO.Data.WebDispatchPerformance.Interfaces;
    using MCO.Data.WebDispatchPerformance;
    using MCO.Services.WebDispatchPerformance;
    using MCO.Services.WebDispatchPerformance.Interfaces;
    using System;
    using System.Configuration;

    public class ApplicationModule : NinjectModule
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

            Bind(typeof(IApp)).To(typeof(App));
            Bind<ILog>().ToMethod(x =>
            {
                var scope = x.Request.ParentRequest.Service.FullName;
                var log = (ILog)LogManager.GetLogger(scope, typeof(Log));
                return log;
            });
            Bind(typeof(IDataHandler)).To(typeof(DataHandler));
            Bind(typeof(IExcelHandler)).To(typeof(ExcelHandler));
            Bind(typeof(IExcelWriter)).To(typeof(ExcelWriter));
            Bind(typeof(IExcelCreater)).To(typeof(ExcelCreater));
            Bind(typeof(IRepository)).To(typeof(OracleRepository)).InSingletonScope().WithConstructorArgument("connectionString", connectionString);
            Bind(typeof(IPerformLookup)).To(typeof(PerformLookup));
            Bind(typeof(IProcessHandler)).To(typeof(ProcessHandler));

            Bind<DispatchDetails>().ToSelf();           
        }
    }
}
