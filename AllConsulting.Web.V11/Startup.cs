using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AllConsulting.Web.Startup))]
namespace AllConsulting.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
