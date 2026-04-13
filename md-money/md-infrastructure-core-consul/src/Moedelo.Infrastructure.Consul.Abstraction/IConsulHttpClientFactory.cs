using System.Net.Http;

namespace Moedelo.Infrastructure.Consul.Abstraction
{
    public interface IConsulHttpClientFactory
    {
        /// <summary>
        /// Создать новый экземпляр типа HttpClient
        /// </summary>
        /// <returns></returns>
        HttpClient CreateHttpClient();
        
        /// <summary>
        /// Признак того, вызывается ли для тела запроса метод Dispose автоматически внутри вызываемого метода HttpClient.
        /// До .net core 3.0 http-клиент вызывал Dispose для тела запроса HttpContent,
        /// Начиная с .net core 3.0 http-клиент не вызывает Dispose для HttpContent, это становится обязанностью вызываемой стороны.
        /// </summary>
        bool IsHttpContentAutoDisposable { get; }
    }
}
