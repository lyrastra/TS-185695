using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.PaymentTransactions;

public class PaymentTransactionUnLinkFromPaymentDto
{
    public int TransactionId { get; set; }

    public PaymentTransactionType Type { get; set; }
}