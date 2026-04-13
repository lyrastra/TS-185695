namespace Moedelo.Billing.Abstractions.AutoBilling.Dto;

public sealed class AddRequestsIntoInitiateRequestDto
{
    public int InitiateId { get; set; }
    public int[] FirmIds { get; set; }
}