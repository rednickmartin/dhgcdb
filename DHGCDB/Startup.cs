using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DHGCDB.Startup))]
namespace DHGCDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
