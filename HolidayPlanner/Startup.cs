using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HolidayPlanner.Startup))]
namespace HolidayPlanner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
        }
    }
}
