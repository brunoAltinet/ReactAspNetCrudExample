using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AltiFinReact.Web.Startup))]

namespace AltiFinReact.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
