using System;
using System.Collections.Generic;
using System.Web;
using Moedelo.InfrastructureV2.AuditMvc.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.AuditMvc.Internals.Extensions;

internal static class AuditSpanExtensions
{
    private const long MinBodyLengthToSaveAsTag = 128;
    private const int MaxUserAgentLength = 255;
    private const long MaxBodyLength = 32768; // 32Кб

    private static readonly MdSerializationSettings JsonSerializationSettings = new MdSerializationSettings
    {
        MaskPropertiesByAttribute = true,
        MaskGenericSensitiveProperties = true
    };

    internal static void EnumerateRequestTags(this HttpRequestBase request,
        IDictionary<string, object> filterContextActionParameters,
        bool includeRequestBody, bool addClientIpAddressTag, bool addUserAgent,
        Action<string, object> addTag)
    {
        addTag("Request.Method", request.HttpMethod);
        if (request.Url != null)
        {
            addTag("Request.OriginalUri", request.Url.OriginalString);
        }

        if (addClientIpAddressTag)
        {
            var ipAddress = request.RequestContext.HttpContext.GetClientIp();
            addTag("Request.IP", ipAddress);
        }

        if (addUserAgent)
        {
            var  userAgent = request.UserAgent;

            if (userAgent != null)
            {
                addTag("Request.UserAgent", userAgent.Length < MaxUserAgentLength
                    ? userAgent
                    : userAgent.Substring(0, MaxUserAgentLength));
            }
        }

        if (includeRequestBody)
        {
            var bodyExtractionResult = TryGetBodyJsonString(request.HttpMethod,
                filterContextActionParameters);
            var hasBodyExtracted = bodyExtractionResult.Key;
            var jsonBody = bodyExtractionResult.Value; 

            if (hasBodyExtracted == false)
            {
                if (string.IsNullOrEmpty(jsonBody) != true)
                {
                    addTag("Request.Body.Extraction.Error", jsonBody);
                }
            }
            else if (string.IsNullOrEmpty(jsonBody) != true)
            {
                if (jsonBody.Length >= MinBodyLengthToSaveAsTag)
                {
                    addTag("Request.Body.Length", jsonBody.Length);
                }

                if (jsonBody.Length > MaxBodyLength)
                {
                    addTag("Request.Body.First1kb", jsonBody.Substring(0, 1024));
                }
                else
                {
                    addTag("Request.Body", jsonBody);
                }
            }
        }
    }

    // ReSharper disable once CollectionNeverUpdated.Local
    private static readonly HashSet<string> MethodsWithoutBody =
        new HashSet<string>(["GET", "DELETE"], StringComparer.InvariantCultureIgnoreCase);
        
    private static KeyValuePair<bool, string> TryGetBodyJsonString(string requestHttpMethod,
        IDictionary<string, object> filterContextActionParameters)
    {
        if (MethodsWithoutBody.Contains(requestHttpMethod))
        {
            return new KeyValuePair<bool, string>(true, string.Empty);
        }

        try
        {
            return new KeyValuePair<bool, string>(true, filterContextActionParameters?.ToJsonString(JsonSerializationSettings));
        }
        catch (Exception exception)
        {
            return new KeyValuePair<bool, string>(false, exception.Message);
        }
    }

    internal static void TryAddResponse(this IAuditSpan span,
        HttpResponseBase response,
        bool treatValidationExceptionAsError)
    {
        span.AddTag("Response.StatusCode", response.StatusCode);

        if (400 <= response.StatusCode && response.StatusCode <= 599)
        {
            if (treatValidationExceptionAsError || response.StatusCode != 422)
            {
                span.SetError();
            }
        }
            
        // response.OutputStream?.Length - не работает, поскольку OutputStream доступен только на запись
    }
}