using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MundiPagg.Api.Products.Infrastructrure.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundiPagg.Api.Products.Api.Component
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exc = new ExceptionError();
            exc.Error = context.Exception.GetBaseException().Message;
            
            context.HttpContext.Response.StatusCode = 400;

            if (context.Exception is DomainException)
            {
                var ex = context.Exception as DomainException;
                context.Exception = ex;
                exc.Message = "Houve um erro de negócio";
            }
            else
            {
               
#if !DEBUG              
                string stack = null;
#else
                string stack = context.Exception.StackTrace;
#endif

                exc.Message = "Erro inesperado no sistema";
                exc.Detail = stack;

                context.HttpContext.Response.StatusCode = 500;

            }

            context.Result = new JsonResult(exc);

            base.OnException(context);
        }

        internal class ExceptionError
        {
            public string Message { get; set; }
            public string Error { get; set; }
            public string Detail { get; set; }
        }
    }
}
