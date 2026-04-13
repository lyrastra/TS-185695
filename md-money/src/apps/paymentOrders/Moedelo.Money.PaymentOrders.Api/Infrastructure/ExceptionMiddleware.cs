using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Exceptions;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;

namespace Moedelo.Money.PaymentOrders.Api.Infrastructure
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
            catch (PaymentOrderNotFoundExcepton)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync("").ConfigureAwait(false);
                return;
            }
            catch (PaymentOrderMismatchTypeExcepton pomtex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                var response = new MismatchOperationTypeResponseDto
                {
                    DocumentBaseId = pomtex.DocumentBaseId,
                    ExpectedType = pomtex.ExpectedType,
                    ActualType = pomtex.ActualType
                };
                await context.Response.WriteAsync(response.ToJsonString()).ConfigureAwait(false);
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
