namespace Moedelo.Billing.Abstractions.Legacy.Dto;

public class PaymentHistoryMarkAsExportedTo1CRequestDto
{
    public int PartnerUserId { get; set; }
    public int[] PaymentIds { get; set; }
}