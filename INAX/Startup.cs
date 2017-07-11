using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(INAX.Startup))]
namespace INAX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
