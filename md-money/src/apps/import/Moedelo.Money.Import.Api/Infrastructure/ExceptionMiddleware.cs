using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Import.Domain.Exceptions;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS1591

namespace Moedelo.Money.Import.Api.Infrastructure
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
                await next(context).ConfigureAwait(true);
            }
            catch (FileValidationException vex)
            {
                context.Response.StatusCode = 422;
                var errors = new
                {
                    errors = vex.Errors
                };
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errors.ToJsonString(), Encoding.UTF8).ConfigureAwait(true);
                return;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Неизвестная ошибка!").ConfigureAwait(true);
            }
        }
    }
}
