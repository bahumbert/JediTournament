using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JediWebApplication.Startup))]
namespace JediWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
