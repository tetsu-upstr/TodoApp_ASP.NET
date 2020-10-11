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

            // Configuration.cs�Őݒ肵���}�C�O���[�V�����������I�ɔ��f������悤�ɒǋL
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoesContext, Configuration>());
        }
    }
}
