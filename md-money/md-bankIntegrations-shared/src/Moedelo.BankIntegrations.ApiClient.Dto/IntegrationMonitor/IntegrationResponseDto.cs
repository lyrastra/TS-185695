using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class IntegrationResponseDto<T>
    {
        public T Data { get; set; }

        public IntegrationResponseStatusCode StatusCode { get; set; }

        public IntegrationResponseDto(T data, IntegrationResponseStatusCode statusCode = IntegrationResponseStatusCode.Ok)
        {
            Data = data;
            StatusCode = statusCode;
        }
    }
}
