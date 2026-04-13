namespace Moedelo.Billing.Abstractions.Dto.PaymentHistorySellers;

public class PaymentHistorySellerDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal ShareSum { get; set; }
    public bool ForDelete { get; set; }
}