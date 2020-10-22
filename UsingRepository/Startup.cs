using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UsingRepository.Startup))]
namespace UsingRepository
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
