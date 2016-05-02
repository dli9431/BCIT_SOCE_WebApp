using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COMP4900_SOCE_WebApp.Startup))]
namespace COMP4900_SOCE_WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
