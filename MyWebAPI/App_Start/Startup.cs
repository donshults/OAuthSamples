using Microsoft.Owin;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(MyWebAPI.App_Start.Startup))]
namespace MyWebAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            

            app.useCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            var azureADBearerAuthOptions = new WindowsAzureActiveDirectoryBearerAuthenticationOptions
            {
                Tenant = ConfigurationManager.AppSettings["ida:Tenant"]
            };
            azureADBearerAuthOptions.TokenValidationParameters =
                new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidAudience = ConfigurationManager.AppSettings["ida:Audience"]
                };
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(azureADBearerAuthOptions);
        }
    }
}