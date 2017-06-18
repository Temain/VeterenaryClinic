using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VeterinaryClinic.Web.Startup))]
namespace VeterinaryClinic.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
