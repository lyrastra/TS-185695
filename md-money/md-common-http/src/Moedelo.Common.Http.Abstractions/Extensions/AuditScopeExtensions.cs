using System.IO;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Common.Http.Abstractions.Extensions;

internal static class AuditScopeExtensions
{
    // internal static void TryAddRequestTag<TRequest>(this IAuditScope scope, TRequest request)
    // {
    //     try
    //     {
    //         if (scope.IsEnabled == false)
    //         {
    //             return;
    //         }
    //         
    //         scope.Span.AddTag("Request", request);
    //     }
    //     catch
    //     {
    //         //ignore
    //     }
    // }
        
    internal static void DumpRequestToTag(this IAuditScope scope, string requestJson)
    {
        const long maxResponseLength = 8192; // 8 Kb
        const int dumpSize = 1024;

        try
        {
            if (scope.IsEnabled == false)
            {
                return;
            }

            if (requestJson == null)
            {
                scope.Span.AddTag("Request.Body", "null");
                return;
            }

            scope.Span.AddTag("Request.Body.Length", requestJson.Length);

            if (requestJson.Length <= maxResponseLength)
            {
                scope.Span.AddTag("Request.Body", requestJson);
            }
            else
            {
                scope.Span.AddTag("Request.Body1Kb", requestJson[..dumpSize]);
            }
        }
        catch
        {
            //ignore
        }
    }

    internal static void DumpResponseToTag(this IAuditScope scope, string responseJson)
    {
        const long maxResponseLength = 8192; // 8 Kb
        const int dumpSize = 1024;

        try
        {
            if (scope.IsEnabled == false)
            {
                return;
            }

            if (responseJson == null)
            {
                scope.Span.AddTag("Response.Body", "null");
                return;
            }

            scope.Span.AddTag("Response.Body.Length", responseJson.Length);

            if (responseJson.Length <= maxResponseLength)
            {
                scope.Span.AddTag("Response.Body", responseJson);
            }
            else
            {
                scope.Span.AddTag("Response.Body1Kb", responseJson[..dumpSize]);
            }
        }
        catch
        {
            //ignore
        }
    }

    internal static void TryAddFileTag(this IAuditScope scope, HttpFileModel fileModel)
    {
        try
        {
            if (scope.IsEnabled == false)
            {
                return;
            }
                
            scope.Span.AddTag("File", new
            {
                Name = fileModel.FileName,
                ContentType = fileModel.ContentType,
            });
        }
        catch
        {
            //ignore
        }
    }
    internal static void TryAddFileTag(this IAuditScope scope, HttpFileStream fileModel)
    {
        try
        {
            if (scope.IsEnabled == false)
            {
                return;
            }
                
            scope.Span.AddTag("File", new
            {
                Name = fileModel.FileName,
                ContentType = fileModel.ContentType,
                Size = fileModel.Stream?.GetStreamLengthOrNull()
            });
        }
        catch
        {
            //ignore
        }
    }

    private static long? GetStreamLengthOrNull(this Stream stream)
    {
        try
        {
            return stream.Length;
        }
        catch
        {
            return null;
        }
    }
}