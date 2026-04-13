using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.PaymentHistorySellers;

public class PaymentHistorySellersUpdateRequestDto
{
    public int PaymentHistoryId { get; set; }
    public decimal PaymentSum { get; set; }
    public IReadOnlyCollection<PaymentHistorySellerDto> Sellers { get; set; }
}