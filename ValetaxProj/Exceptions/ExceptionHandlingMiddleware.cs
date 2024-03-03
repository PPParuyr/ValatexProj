using Azure.Core;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.OpenApi.Extensions;
using System.Net;
using System.Text.Json;
using ValetaxProj.Models;
using ValetaxProj.Services.Interfaces;

namespace ValetaxProj.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private IExceptionService _exceptionService;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IExceptionService exceptionService)
        {
            _exceptionService = exceptionService;
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            context.Request.EnableBuffering();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            using StreamReader stream = new StreamReader(context.Request.Body);

            string bodyText = await stream.ReadToEndAsync();


            Guid eventId = Guid.NewGuid();
            var errorResponse = new ErrorResponse();
            switch (exception)
            {
                case SecureException ex:
                    errorResponse.Id = eventId;
                    errorResponse.Type = ErrorType.Secure.GetDisplayName();
                    errorResponse.Data = new ErrorMessage
                    {
                        Message = ex.Message
                    };
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    errorResponse.Id = eventId;
                    errorResponse.Type = ErrorType.Exception.GetDisplayName();
                    errorResponse.Data = new ErrorMessage
                    {
                        Message = $"Internal server error ID = {eventId}"
                    };
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            ExceptionsLog exceptionsLog = new ExceptionsLog
            {
                Id = eventId,
                Time = DateTime.Now,
                QueryParametres = context.Request.QueryString.ToString(),
                BodyParametres = bodyText,
                StackTrace = exception.StackTrace
            };
            await _exceptionService.WriteException(exceptionsLog);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
