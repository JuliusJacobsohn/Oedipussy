using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ÖdipussyWeb.Startup))]
namespace ÖdipussyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
