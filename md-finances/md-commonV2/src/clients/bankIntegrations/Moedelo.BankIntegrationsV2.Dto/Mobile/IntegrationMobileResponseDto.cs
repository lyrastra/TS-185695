namespace Moedelo.BankIntegrationsV2.Dto.Mobile
{
    public class IntegrationMobileResponseDto<T>
    {
        public T Data { get; set; }
        public bool ResponseStatus { get; set; }
        public int StatusCode { get; set; }
        public string ResponseMessage { get; set; }
        
        public IntegrationMobileResponseDto(T data)
        {
            Data = data;
        }
    }
}