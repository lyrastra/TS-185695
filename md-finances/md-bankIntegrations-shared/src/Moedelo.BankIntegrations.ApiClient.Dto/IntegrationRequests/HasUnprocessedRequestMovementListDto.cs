namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class HasUnprocessedRequestMovementListDto
{
    public int FirmId { get; set; }
    public string DateOfCallAfter { get; set; }
    public bool? IsManual { get; set; }
}