using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BancoTabajara.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Método responsável por validar a autenticação de cada chamada realizada.
        /// </summary>
        /// <param name="actionContext">É o contexto atual da chamada Http recebida</param>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // Verifica se o token é válido pela validação padrão do oauth
            // poderia acessar o banco, consultar outro ws e etc conforme necessário
            return base.IsAuthorized(actionContext);
        }
    }
}