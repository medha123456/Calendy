using Calendy_Login.Services;
using Calendy_Login.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.Practices.Unity;
using System.Web.Http;


namespace Calendy_Login
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEventServices, EventServices>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            
            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");           
            //config.EnableCors(cors);
            // Web API configuration and services
            config.Routes.MapHttpRoute(
               name: "swagger_root",
               routeTemplate: "",
               defaults: null,
               constraints: null,
               handler: new Swashbuckle.Application.RedirectHandler(
                   (message => message.RequestUri.ToString()), "swagger")
               );
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
