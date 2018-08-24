using BancoTabajara.API.IoC;
using BancoTabajara.Applications.Features.Clientes;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using SimpleInjector.Lifestyles;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BancoTabajara.API.Provider
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            
            using (AsyncScopedLifestyle.BeginScope(SimpleInjectorInitializer.ContainerInstance))
            {
                var service = SimpleInjectorInitializer.ContainerInstance.GetInstance<IClienteService>();
                var user = service.Login(context.UserName, context.Password);
            
                if (user == null)
                {
                    context.SetError("invalid_grant", "Usuário ou senha invalidos");
                    return;
                }
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            context.Validated(identity);
        }
    }
}