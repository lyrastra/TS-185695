using System;
using System.Buffers;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Middleware.Attributes;
using Moedelo.Common.Audit.Middleware.Internals;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Audit.Middleware.Extensions;

internal static class AuditSpanExtensions
{
    private const long MaxBodyLength = 8 * 1024;
    private const int MinBodyLengthToTag = 128;
    private static readonly int[] RentCapacities = [64, 128, 512, 1024, (int)MaxBodyLength];

    private static int GetArraySizeToRent(int capacity)
    {
        foreach (var limit in RentCapacities)
        {
            if (capacity <= limit)
            {
                return limit;
            }
        }

        return capacity;
    }

    internal static async Task TryAddRequestAsync(this IAuditSpan span,
        HttpRequest httpRequest,
        bool includeBody,
        bool tagClientIpAddress)
    {
        span.AddTag("Request.DisplayUrl", httpRequest.GetDisplayUrl());
        span.AddTag("Request.Method", httpRequest.Method);

        if (tagClientIpAddress)
        {
            var clientIpAddress = httpRequest.HttpContext.GetClientIp(allowNull: true);

            if (clientIpAddress != null)
            {
                span.AddTag("Request.IP", clientIpAddress);
            }
        }

        if (includeBody)
        {
            await TryAddRequestBodyAsync(span, httpRequest).ConfigureAwait(false);
        }
    }

    private static async Task TryAddRequestBodyAsync(
        IAuditSpan span,
        HttpRequest httpRequest)
    {
        if (httpRequest.Body is { CanRead: false } or { CanSeek: false })
        {
            return;
        }

        var contentType = httpRequest.ContentType;
        var longContentLength = httpRequest.ContentLength;

        if (AuditContentTypeHelper.CanDumpContentWithContentType(contentType) == false)
        {
            if (string.IsNullOrEmpty(contentType) == false)
            {
                span.AddTag("Request.ContentType", contentType);
            }

            if (longContentLength > 0)
            {
                span.AddTag("Request.Body.Length", longContentLength);
            }

            return;
        }

        if (longContentLength >= MaxBodyLength)
        {
            span.AddTag("Request.Body.Length", longContentLength);
            return;
        }

        httpRequest.EnableBuffering();

        var contentLength = Convert.ToInt32(longContentLength);
        var buffer = ArrayPool<byte>.Shared.Rent(GetArraySizeToRent(contentLength));

        try
        {
            contentLength = await httpRequest.Body.ReadAsync(buffer);
            var requestBodyString = Encoding.UTF8.GetString(buffer, 0, contentLength);
            span.AddTag("Request.Body", requestBodyString);
        }
        catch
        {
            //ignore
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer, true);
            httpRequest.Body.Position = 0;
        }
    }

    internal static void TagResponseStatusCode(
        this IAuditSpan span,
        int statusCode,
        bool treatValidationExceptionAsError)
    {
        const int minErrorCode = 400;
        const int maxErrorCode = 599;
        const int validationErrorCode = 422;

        span.AddTag("Response.StatusCode", statusCode);

        if (statusCode is >= minErrorCode and <= maxErrorCode)
        {
            if (treatValidationExceptionAsError || statusCode != validationErrorCode)
            {
                span.SetError();
            }
        }
    }

    internal static void TagResponseStatusCode(
        this IAuditSpan span,
        HttpContext httpContext,
        bool treatValidationExceptionAsError)
    {
        span.AddTag("Response.StatusCode", httpContext.Response.StatusCode);

        if (httpContext.IsAuditTrailErrorStatusCode(treatValidationExceptionAsError))
        {
            span.SetError();
        }
    }

    private static bool IsAuditTrailErrorStatusCode(
        this HttpContext httpContext,
        bool treatValidationExceptionAsError)
    {
        const int minErrorCode = 400;
        const int maxErrorCode = 599;

        var statusCode = httpContext.Response.StatusCode;

        if (statusCode is < minErrorCode or > maxErrorCode)
        {
            // не ошибка
            return false;
        }

        var httpStatusCode = (HttpStatusCode)statusCode;

        if (httpStatusCode == HttpStatusCode.UnprocessableEntity && treatValidationExceptionAsError == false)
        {
            return false;
        }

        var endpoint = httpContext.GetEndpoint();
        var notErrorsAttributes = endpoint?.Metadata.GetOrderedMetadata<MdAuditTrailIsNotErrorAttribute>();

        if (notErrorsAttributes is not { Count: > 0 })
        {
            // нет атрибутов, влияющих на принятие решения
            return true;
        }

        return notErrorsAttributes.All(attribute => !attribute.StatusCodes.Contains(httpStatusCode));
    }

    internal static void AddResponseBody(
        this IAuditSpan span,
        ObjectResult result)
    {
        if (result?.Value == null)
        {
            return;
        }

        using var jsonStream = result.Value.ToJsonStream(new StreamLengthCalculator());

        if (jsonStream.Length >= MinBodyLengthToTag)
        {
            span.AddTag("Response.Body.Length", jsonStream.Length);
        }

        if (jsonStream.Length >= MaxBodyLength)
        {
            // слишком большое тело, чтобы логировать его целиком
            return;
        }

        span.AddTag("Response.Body", result.Value);
    }
}
