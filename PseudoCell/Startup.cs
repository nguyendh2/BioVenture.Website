using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PseudoCell.Startup))]
namespace PseudoCell
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
