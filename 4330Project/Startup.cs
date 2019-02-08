using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_4330Project.Startup))]
namespace _4330Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
