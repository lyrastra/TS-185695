using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Infrastructure.Http.Abstractions.Interfaces
{
    public interface IHttpRequestExecuter
    {
        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<string> GetAsync(string uri,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);

        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<string> PostAsync(
            string uri,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);

        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<string> PostAsync(
            string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);
        
        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<string> PutAsync(
            string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);

        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<string> PatchAsync(
            string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);

        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<string> DeleteAsync(
            string uri,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);

        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<string> DeleteAsync(
            string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);

        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<HttpFileModel> DownloadFileAsync(
            string uri,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);
        
        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<HttpFileModel> DownloadFileAsync(
            string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Загрузить файл
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod">http-метод, который должен быть использован</param>
        /// <param name="dataHttpContent">тело запроса</param>
        /// <param name="headers">заголовки запроса</param>
        /// <param name="setting">настройки выполнения запроса</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <typeparam name="TRequestBody">тип тела запроса. Если тела запроса нет, то следует указывать object</typeparam>
        /// <returns>структура описания файла или null, если файл не найден</returns>
        Task<HttpFileStream> DownloadFileAsync<TRequestBody>(string uri,
            HttpMethod httpMethod,
            HttpContent dataHttpContent,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);


        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<string> UploadFileAsync(
            string uri, 
            HttpFileModel file, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);
        
        /// <exception cref="Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Exceptions.HttpRequestTimeoutException"></exception>
        Task<string> UploadFileAsync(
            string uri,
            HttpContent dataHttpContent,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);

        Task<string> UploadFileAsync(
            string uri,
            HttpContent dataHttpContent,
            HttpFileStream file,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default);

    }
}