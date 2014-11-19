using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MathPro.WebUI.Startup))]
namespace MathPro.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);
        }
    }
}
