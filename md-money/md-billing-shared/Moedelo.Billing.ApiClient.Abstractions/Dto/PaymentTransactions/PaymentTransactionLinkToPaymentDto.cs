using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.PaymentTransactions;

public class PaymentTransactionLinkToPaymentDto
{
    public int TransactionId { get; set; }

    public PaymentTransactionType Type { get; set; }

    public int PaymentHistoryId { get; set; }

    public int FirmId { get; set; }
}