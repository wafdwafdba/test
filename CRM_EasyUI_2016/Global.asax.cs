using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GroupThink.CRM.Core;

namespace CRM_EasyUI_2016
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //启用压缩
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AuthConfig.RegisterAuth();

            //注入 Ioc
            var container = GroupThink.CRM.Common.Constant.container;// new UnityContainer();
            DependencyRegisterType.Container_Sys(ref container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            GroupThink.CRM.Common.ResultHelper.LoadLicense();

            //ISystemConfig monitor = container.Resolve<ISystemConfig>();
            //monitor.GetSystemConfig();

            //AutoMapper映射初始化
            GroupThink.CRM.Core.InitAutoMapper.Init();
        }
    }
}