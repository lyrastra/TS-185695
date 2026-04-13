namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation
{
    public class RequestMovementListResponseDto
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public int IntegrationRequestId { get; set; }
    }
}
