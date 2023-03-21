using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(W2022IG.Startup))]

namespace W2022IG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
