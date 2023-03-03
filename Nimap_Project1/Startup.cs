using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nimap_Project1.Startup))]
namespace Nimap_Project1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
