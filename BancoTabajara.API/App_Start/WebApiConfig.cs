using BancoTabajara.API.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Web.Http.ExceptionHandling;

namespace BancoTabajara.API.App_Start
{
    [ExcludeFromCodeCoverage]
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MapApiRoutes();
            config.ConfigureJsonSerialization();
            config.ConfigureXMLSerialization();
            // Adicionamos o atributo global de tratamento de exceções 
            // para responder adequadamente conforme exception de negócio
            config.Filters.Add(new ExceptionHandlerAttribute());
        }

        private static void MapApiRoutes(this HttpConfiguration config)
        {
            // Habilita o uso do Atributo de [Route]
            config.MapHttpAttributeRoutes();
            // Configura o uso de [Route]
            config.Routes.MapHttpRoute(
                name: "BancoTabajara.API",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    action = RouteParameter.Optional
                }
            );
        }

        private static void ConfigureJsonSerialization(this HttpConfiguration config)
        {
            var jsonSerializerSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSerializerSettings.Formatting = Formatting.None;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            // Define que o Content-Type na resposta é o application/json
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        private static void ConfigureXMLSerialization(this HttpConfiguration config)
        {
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}