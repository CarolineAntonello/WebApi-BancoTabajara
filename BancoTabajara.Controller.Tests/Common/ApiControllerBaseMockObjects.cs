using BancoTabajara.API.Controllers.Common;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BancoTabajara.Controller.Tests.Common
{
    public class ApiControllerBaseFake : ApiControllerBase
    {
        public IHttpActionResult HandleCallback<TSuccess>(Func<TSuccess> func)
        {
            return base.HandleCallback(func);
        }

        public IHttpActionResult HandleQuery<TSource, TResult>(TSource query)
        {
            return base.HandleQuery<TSource, TResult>(query);
        }

        public IHttpActionResult HandleQueryable<TSource, TResult>(IQueryable<TSource> query)
        {
            return base.HandleQueryable<TSource, TResult>(query);
        }

        public IHttpActionResult HandleValidationFailure<T>(IList<T> validationFailure) where T : ValidationFailure
        {
            return base.HandleValidationFailure<T>(validationFailure);
        }
    }

    public class ApiControllerBaseDummy { }
    public class ApiControllerBaseDummyViewModel { }

    public class ApiControllerBaseDummyQuery { }
}