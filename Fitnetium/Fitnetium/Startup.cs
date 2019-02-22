using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fitnetium.Startup))]
namespace Fitnetium
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
