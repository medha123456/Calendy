using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using Calendy_Login;
using Calendy_Login.App_Start;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Calendy_Login.App_Start
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "calendy");
                })
            .EnableSwaggerUi(c =>
            {
            });
        }
    }
}