using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CapitecPayRoll.Startup))]
namespace CapitecPayRoll
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
