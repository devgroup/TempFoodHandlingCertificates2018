using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TempFoodHandlingCertificates2014.Startup))]
namespace TempFoodHandlingCertificates2014
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
