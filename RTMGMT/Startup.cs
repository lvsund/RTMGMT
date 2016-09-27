using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RTMGMT.Startup))]
namespace RTMGMT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
