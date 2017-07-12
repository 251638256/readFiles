using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SourceCode.Startup))]
namespace SourceCode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
