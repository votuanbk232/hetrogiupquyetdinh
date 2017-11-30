using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hetrogiupquyetdinh.Startup))]
namespace Hetrogiupquyetdinh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
