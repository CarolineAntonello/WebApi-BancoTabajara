using BancoTabajara.API;
using BancoTabajara.API.App_Start;
using BancoTabajara.Applications.Features.Clientes;
using BancoTabajara.Applications.Features.Contas;
using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Infra.ORM.Context;
using BancoTabajara.Infra.ORM.Features.Clientes;
using BancoTabajara.Infra.ORM.Features.Contas;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebActivatorEx;

namespace BancoTabajara.API.IoC
{
    public static class SimpleInjectorInitializer
    {
        public static Container ContainerInstance { get; private set; }
        public static void Initialize()
        {
            ContainerInstance = new Container();

            ContainerInstance.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            RegisterServices(ContainerInstance);

            ContainerInstance.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            ContainerInstance.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(ContainerInstance);
        }

        public static void RegisterServices(Container container)
        {
            #region Services
            container.Register<IClienteService, ClienteService>(Lifestyle.Scoped);
            container.Register<IContaService, ContaService>(Lifestyle.Scoped);
            #endregion

            #region Repository
            container.Register<IClienteRepository, ClienteRepository>(Lifestyle.Scoped);
            container.Register<IContaRepository, ContaRepository>(Lifestyle.Scoped);
            #endregion

            #region Context
            container.Register(() => new BancoTabajaraContext(), Lifestyle.Scoped);
            #endregion

        }

    }
}