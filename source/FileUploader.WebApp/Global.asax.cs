using System;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FileUploader.WebApp.DAL;

namespace FileUploader.WebApp
{
    public class MvcApplication : HttpApplication
    {
        public static int MaxRequestLengthInKB
        {
            get
            {
                HttpRuntimeSection section =
                    ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
                return section != null ? section.MaxRequestLength : 4096;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new FileStatisticsInitializer());
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var httpException = ex as HttpException ?? ex.InnerException as HttpException;
            if (httpException == null) return;

            if (httpException.WebEventCode == WebEventCodes.RuntimeErrorPostTooLarge)
            {
                Server.ClearError();
                Response.Write(string.Format("Dear user\n\n" +
                                             "I'm sorry, but the file you want to give me is way too big. I can't handle it.\n\n" +
                                             "Yours 'til the cat meows,\n" +
                                             "Server\n\n" +
                                             "P.S. The limit is set to {0}KB", MaxRequestLengthInKB));
            }
        }
    }
}