using CircitApi.Infrastructure.Models;
using CircitApi.Infrastructure.Services;
using CircitApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CircitApi.Infrastructure.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IEventLogger Logger;


        public CustomExceptionFilter(IEventLogger logger)
        {
            Logger = logger;
        }


        public override void OnException(ExceptionContext context)
        {
            int statusCode = 500;
            string errorMessage = "An unhandled error occurred.";

            if (context.Exception is CustomException)
            {
                var ex = context.Exception as CustomException;

                if (ex.Exception != null)
                {
                    errorMessage = ex.Exception.Message;

                    Logger.WriteErrorEvent(ex.SourceName, ex.SourceMethod, ex.Exception, ex.ErrorNumber);
                }
                else
                {
                    errorMessage = ex.ExceptionMessage;

                    Logger.WriteErrorEvent(ex.SourceName, ex.SourceMethod, ex.ExceptionMessage, ex.ErrorNumber);
                }
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                statusCode = 401;

                errorMessage = "Unauthorized Access";

                Logger.WriteErrorEvent(errorMessage, "See exception details", context.Exception, 4001);
            }
            else
            {
                Logger.WriteErrorEvent("Unhandled Exception", "See exception details", context.Exception, 5001);
            }

            ErrorResponse errorResponse = new ErrorResponse
            {
                ErrorMessage = errorMessage
            };

#if DEBUG
            errorResponse.StackDetails = context.Exception.GetBaseException().Message + " ***** " + context.Exception.StackTrace;
#endif

            context.ExceptionHandled = true;
            context.Exception = null;
            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new JsonResult(errorResponse) { StatusCode = statusCode };

            base.OnException(context);
        }
    }
}