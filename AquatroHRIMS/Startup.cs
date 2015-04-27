using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AquatroHRIMS.Startup))]
namespace AquatroHRIMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
