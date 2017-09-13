using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToyRobot.Web.Startup))]
namespace ToyRobot.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
