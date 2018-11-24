using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VirualLab.Startup))]
namespace VirualLab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
