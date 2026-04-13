using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Internals;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Extensions;

internal static class AuditScopeExtensions
{
    private const int MaxResponseLength = 8192; // 8 Kb
    private const int MinResponseBodyLengthToTag = 128;
        
    internal static IAuditScope TryAddRequestTag<TRequest>(this IAuditScope scope, TRequest request) 
        where TRequest : class
    {
        if (scope.IsEnabled)
        {
            scope.Span.AddTag("Request", request);
        }

        return scope;
    }

    internal static void TryAddResponseTag(this IAuditScope scope, Stream responseJsonStream)
    {
        if (scope is not {IsEnabled: true})
        {
            return;
        }

        try
        {
            if (responseJsonStream == null)
            {
                scope.Span.AddTag("Response.Body", "null");
                return;
            }

            if (responseJsonStream.Length >= MinResponseBodyLengthToTag)
            {
                scope.Span.AddTag("Response.Body.Length", responseJsonStream.Length);
            }

            if (responseJsonStream.Position != 0L)
            {
                Debug.Assert(responseJsonStream.CanSeek);
                responseJsonStream.Seek(0, SeekOrigin.Begin);
            }

            using var textReader = new StreamReader(
                responseJsonStream,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: 1024,
                leaveOpen: true);

            if (responseJsonStream.Length <= MaxResponseLength)
            {
                scope.Span.AddTag("Response.Body", textReader.ReadToEnd());
            }
            else
            {
                var buffer = new char[MaxResponseLength];
                var readCount = textReader.ReadBlock(buffer, 0, MaxResponseLength);
                scope.Span.AddTag("Response.Body1Kb", new string(buffer, 0, readCount));
            }
            responseJsonStream.Seek(0, SeekOrigin.Begin);
        }
        catch
        {
            //ignore
        }
        finally
        {
            if (responseJsonStream?.Position > 0L)
            {
                responseJsonStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }

    internal static void TryAddResponseTag(this IAuditScope scope, string responseJson)
    {
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

            if (responseJson.Length >= MinResponseBodyLengthToTag)
            {
                scope.Span.AddTag("Response.Body.Length", responseJson.Length);
            }

            if (responseJson.Length <= MaxResponseLength)
            {
                scope.Span.AddTag("Response.Body", responseJson);
            }
            else
            {
                scope.Span.AddTag("Response.Body1Kb", responseJson.Substring(0, 1024));
            }
        }
        catch
        {
            //ignore
        }
    }

    internal static void SetErrorExceptCancellation(this IAuditScope scope, Exception exception)
    {
        if (exception is OperationCanceledException)
        {
            scope.Span.AddTag("IsOperationCanceled", true);
        }
        else
        {
            scope.Span.SetError(exception);
        }
    }

    internal static IAuditScope TryAddRequestOrFileTag<TRequest>(this IAuditScope scope, TRequest request) where TRequest : class
    {
        return request switch
        {
            IFileModelWithMetadata file => scope.TryAddFileTag(file),
            IFileStreamWithMetadata file => scope.TryAddFileTag(file),
            HttpFileModel file => scope.TryAddFileTag(file),
            HttpFileStream file => scope.TryAddFileTag(file),
            _ => scope.TryAddRequestTag(request)
        };
    }

    private static IAuditScope TryAddFileTag(this IAuditScope scope, IFileModelWithMetadata file)
    {
        scope.TryAddFileTag(file.File);
        return scope.TryAddRequestTag(file.Metadata);
    }
    
    private static IAuditScope TryAddFileTag(this IAuditScope scope, IFileStreamWithMetadata file)
    {
        scope.TryAddFileTag(file.FileStream);
        return scope.TryAddRequestTag(file.Metadata);
    }

    internal static IAuditScope TryAddFileTag(this IAuditScope scope, HttpFileModel fileModel)
    {
        try
        {
            if (scope.IsEnabled == false)
            {
                return scope;
            }
                
            scope.Span.AddTag("File", new
            {
                Name = fileModel.FileName,
                ContentType = fileModel.ContentType,
                Size = fileModel.Stream.GetLengthOrNullSafely()
            });
        }
        catch
        {
            //ignore
        }

        return scope;
    }

    internal static IAuditScope TryAddFileTag(this IAuditScope scope, HttpFileStream fileModel)
    {
        try
        {
            if (scope.IsEnabled == false)
            {
                return scope;
            }
                
            scope.Span.AddTag("File", new
            {
                Name = fileModel.FileName,
                ContentType = fileModel.ContentType,
                Size = fileModel.Stream.GetLengthOrNullSafely()
            });
        }
        catch
        {
            //ignore
        }

        return scope;
    }
}
