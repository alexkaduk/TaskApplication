using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;
using TaskApplication.Models;
using TaskApplication.Common;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.Services.Concrete;
using TaskApplication.Services.Interfaces;

namespace TaskApplication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            XmlConfigurator.Configure();

            Database.SetInitializer(new TaskContexInitializer());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            iocInit();
        }

        private void iocInit()
        {
            Ioc.Init((kernel) =>
            {
                kernel.Bind<ICategoryService>().To<CategoryService>().InTransientScope();
                kernel.Bind<IIssueService>().To<IssueService>().InTransientScope();
                kernel.Bind<IStatusService>().To<StatusService>().InTransientScope();
                kernel.Bind<ISubTaskService>().To<SubTaskService>().InTransientScope();

                kernel.Bind<ICategoryRepository>().To<CategoryRepository>().InTransientScope();
                kernel.Bind<IIssueRepository>().To<IssueRepository>().InTransientScope();
                kernel.Bind<IStatusRepository>().To<StatusRepository>().InTransientScope();
                kernel.Bind<ISubTaskRepository>().To<SubTaskRepository>().InTransientScope();
            });
        }

    }
    
}