using BancoTabajara.API.Exceptions;
using BancoTabajara.Domain.Exceptions;
using System;
using System.Net;
using System.Web.Http;
using FluentValidation.Results;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Net.Http.Headers;
using System.Collections.Generic;
using AutoMapper;

namespace BancoTabajara.API.Controllers.Common
{
    public class ApiControllerBase : ApiController
    {

        protected IHttpActionResult HandleCallback<TSuccess>(Func<TSuccess> func)
        {
            try
            {
                return Ok(func());
            }
            catch (Exception e)
            {
                return HandleFailure(e);
            }
        }

        protected IHttpActionResult HandleQuery<TSource, TResult>(TSource result)
        {
            return Ok(Mapper.Map<TSource, TResult>(result));
        }


        protected IHttpActionResult HandleQueryable<TSource, TResult>(IQueryable<TSource> query)
        {
            var dataQuery = query.ToList().AsQueryable().ProjectTo<TResult>();
            return Ok(dataQuery.ToList());
        }

        protected IHttpActionResult HandleFailure<T>(T exceptionToHandle) where T : Exception
        {
            var exceptionPayload = ExceptionPayload.New(exceptionToHandle);
            return exceptionToHandle is BusinessException ?
                Content(HttpStatusCode.BadRequest, exceptionPayload) :
                Content(HttpStatusCode.InternalServerError, exceptionPayload);
        }

        protected IHttpActionResult HandleValidationFailure<T>(IList<T> validationFailure) where T : ValidationFailure
        {
            return Content(HttpStatusCode.BadRequest, validationFailure);
        }

    }
}