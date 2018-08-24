using BancoTabajara.API.App_Start;
using BancoTabajara.API.IoC;
using BancoTabajara.API.Logger;
using BancoTabajara.Applications.Mapping;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(BancoTabajara.API.Startup))]
namespace BancoTabajara.API
{
    [ExcludeFromCodeCoverage]
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Realiza as configurações gerais da API
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SimpleInjectorInitializer.Initialize();
            AutoMapperInitializer.Initialize();
            // Cria a configuração da api
            HttpConfiguration config = new HttpConfiguration();
            // Configura a autenticação
            OAuthConfig.ConfigureOAuth(app);

            config.MessageHandlers.Add(new CustomLogHandler());

            // Inicia a API com as configurações
            app.UseWebApi(config);

        }
    }
}