using System.Runtime.CompilerServices;

namespace Moedelo.Common.Http.Generic.Abstractions;

public interface IMoedeloBaseApiClient
{
    Task PostAsync<TRequest>(
        string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = default,
        HttpQuerySetting? setting = default,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class;

    Task<TResponse> PostAsync<TResponse>(
        string uri,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task PostAsync(
        string uri,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = default,
        HttpQuerySetting? setting = default,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
        
    Task<TResponse> PostAsync<TRequest, TResponse>(
        string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = default,
        HttpQuerySetting? setting = default,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class;

    Task<TResponse> GetAsync<TResponse>(
        string uri,
        object? queryParams = default,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = default,
        HttpQuerySetting? setting = default,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task DeleteAsync(
        string uri,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = default,
        HttpQuerySetting? setting = default,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TResponse> UploadFileAsync<TRequest, TResponse>(
        string uri,
        TRequest requestBody,
        HttpFileModel file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = default,
        HttpQuerySetting? setting = default,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class;

    Task<HttpFileWithMetadataModel<TMetadata>> DownloadFileWithMetadataAsync<TMetadata>(
        string uri,
        object? queryParams = null,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = default,
        HttpQuerySetting? setting = default,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);//where TMetadata : class;

    /// <summary>
    /// Скачать файл с помощью метода POST
    /// </summary>
    Task<HttpFileWithMetadataModel<TMetadata>> DownloadFileWithMetadataByPostMethodAsHttpFileModelAsync<TMetadata, TRequest>(
        string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = default,
        HttpQuerySetting? setting = default,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TRequest : class;
}