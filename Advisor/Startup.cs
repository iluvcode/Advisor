using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Advisor.Startup))]
namespace Advisor
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
