using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moedelo.Money.PurseOperations.Business.Abstractions.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Moedelo.Money.PurseOperations.Api.Infrastructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
            catch (PurseOperationNotFoundExcepton)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync("").ConfigureAwait(false);
                return;
            }
            catch (PurseOperationMismatchTypeExcepton)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict; // todo: я не знаю что возвращать :-)
                await context.Response.WriteAsync("").ConfigureAwait(false);
                return;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("").ConfigureAwait(false);
            }
        }
    }
}
