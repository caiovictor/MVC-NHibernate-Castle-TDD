using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAPI.Infra;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private WindsorContainer _Wincontainer;

        protected void Application_Start()
        {
            InitializeWindsor();

            //WebApi
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            if (_Wincontainer != null)
                _Wincontainer.Dispose();
        }

        public void InitializeWindsor()
        {
            _Wincontainer = new WindsorContainer();
            _Wincontainer.Install(FromAssembly.This());

            GlobalConfiguration.Configuration.DependencyResolver = new CastleConfig(_Wincontainer.Kernel);
        }

    }
}
