using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvestAdvisor.Web.Landing.Startup))]
namespace InvestAdvisor.Web.Landing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
