using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using TodoApp.Models;
using Configuration = TodoApp.Migrations.Configuration;

namespace TodoApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Configuration.csで設定したマイグレーションを自動的に反映させるように追記
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoesContext, Configuration>());
        }
    }
}
