using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedFile
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
