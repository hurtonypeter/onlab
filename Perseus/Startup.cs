using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Perseus.Startup))]
namespace Perseus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
