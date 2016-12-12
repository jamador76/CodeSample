using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TCRC.Startup))]
namespace TCRC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
