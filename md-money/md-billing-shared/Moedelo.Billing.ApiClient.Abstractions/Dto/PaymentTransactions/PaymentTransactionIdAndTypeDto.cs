using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.PaymentTransactions;

public class PaymentTransactionIdAndTypeDto
{
    public int Id { get; set; }

    public PaymentTransactionType Type { get; set; }
}