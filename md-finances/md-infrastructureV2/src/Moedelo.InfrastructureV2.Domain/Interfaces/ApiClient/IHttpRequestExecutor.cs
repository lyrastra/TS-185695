using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;

public interface IHttpRequestExecutor
{
    Task<string> GetAsync(string uri,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<TResponse> GetAsync<TResponse, TDeserializerArg1>(
        string uri,
        Func<TDeserializerArg1, Stream, TResponse> contentDeserializer,
        TDeserializerArg1 deserializerArg1,
        IReadOnlyCollection<KeyValuePair<string, string>> headers,
        HttpQuerySetting setting,
        CancellationToken cancellationToken);

    Task<string> PostAsync(string uri,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<TResponse> PostAsync<TResponse, TDeserializerArg1, TDeserializerArg2>(string uri,
        Func<TDeserializerArg1, TDeserializerArg2, Stream, TResponse> contentDeserializer,
        TDeserializerArg1 deserializerArg1,
        TDeserializerArg2 deserializerArg2,
        IReadOnlyCollection<KeyValuePair<string, string>> headers,
        HttpQuerySetting setting,
        CancellationToken cancellationToken);

    Task<string> PostAsync<TRequest>(string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default) where TRequest : class;
        
    Task<TResponse> PostAsync<TRequest, TResponse, TDeserializerArg1, TDeserializerArg2>(string uri,
        TRequest data,
        Func<TDeserializerArg1, TDeserializerArg2, Stream, TResponse> contentDeserializer,
        TDeserializerArg1 deserializerArg1,
        TDeserializerArg2 deserializerArg2,
        IReadOnlyCollection<KeyValuePair<string, string>> headers,
        HttpQuerySetting setting,
        CancellationToken cancellationToken) where TRequest : class;
        
    Task<string> PostAsFormUrlEncodedAsync(string uri,
        IReadOnlyList<KeyValuePair<string, string>> data,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<string> PostBodyAsync(string uri,
        string data,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<TResponse> PostBodyAsync<TResponse, TDeserializerArg1, TDeserializerArg2>(
        string uri,
        string data,
        Func<TDeserializerArg1, TDeserializerArg2, Stream, TResponse> contentDeserializer,
        TDeserializerArg1 deserializerArg1,
        TDeserializerArg2 deserializerArg2,
        IReadOnlyCollection<KeyValuePair<string, string>> headers,
        HttpQuerySetting setting,
        CancellationToken cancellationToken);

    Task<string> PutAsync(string uri,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<string> PutAsync<TRequest>(string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default) where TRequest : class;

    Task<TResponse> PutAsync<TRequest, TResponse, TDeserializerArg1, TDeserializerArg2>(string uri,
        TRequest data,
        Func<TDeserializerArg1, TDeserializerArg2, Stream, TResponse> contentDeserializer,
        TDeserializerArg1 deserializerArg1,
        TDeserializerArg2 deserializerArg2,
        IReadOnlyCollection<KeyValuePair<string, string>> headers,
        HttpQuerySetting setting,
        CancellationToken cancellationToken) where TRequest : class;

    Task<HttpFileModel> SendFileAsync<TRequest>(
        string uri, 
        HttpMethod httpMethod, 
        TRequest data = null, 
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default) where TRequest : class;

    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="httpMethod">http-метод, который должен быть использован</param>
    /// <param name="requestBody">тело запроса (может быть null, если тело запроса не требуется)</param>
    /// <param name="headers">заголовки запроса</param>
    /// <param name="setting">настройки выполнения запроса</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <typeparam name="TRequestBody">тип тела запроса. Если тела запроса нет, то следует указывать object</typeparam>
    /// <returns>структура описания файла или null, если файл не найден</returns>
    Task<HttpFileStream> DownloadFileAsync<TRequestBody>(string uri,
        HttpMethod httpMethod,
        TRequestBody requestBody = default,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<HttpFileStreamMetadata<TMetadata>> DownloadFileWithMetadataAsync<TRequestBody, TMetadata, TDeserializerArg1>(
        string uri,
        HttpMethod httpMethod,
        Func<TDeserializerArg1, Stream, TMetadata> metadataDeserializer,
        TDeserializerArg1 deserializerArg1,
        TRequestBody requestBody = default,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<string> PutFileAsync(string uri,
        HttpFileStream file,
        IReadOnlyCollection<KeyValuePair<string, string>> headers,
        HttpQuerySetting setting,
        CancellationToken cancellationToken);

    Task<string> UploadFileAsync(string uri,
        HttpFileStream file,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<string> UploadFileAsync<TRequest>(string uri,
        TRequest data,
        HttpFileStream file,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default) where TRequest : class;

    Task<string> DeleteAsync(string uri,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<string> DeleteAsync<TRequest>(string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default) where TRequest : class;
}