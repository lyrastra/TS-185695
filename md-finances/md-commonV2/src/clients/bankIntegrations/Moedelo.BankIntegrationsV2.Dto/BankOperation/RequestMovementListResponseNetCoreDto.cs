namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class RequestMovementListResponseNetCoreDto
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public int IntegrationRequestId { get; set; }
    }
}
