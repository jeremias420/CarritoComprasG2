using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarritowebG2.Startup))]
namespace CarritowebG2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
