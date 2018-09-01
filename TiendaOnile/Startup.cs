using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TiendaOnile.Startup))]
namespace TiendaOnile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
