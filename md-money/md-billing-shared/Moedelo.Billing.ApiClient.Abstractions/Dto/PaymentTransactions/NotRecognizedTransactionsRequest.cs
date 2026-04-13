using System;

namespace Moedelo.Billing.Abstractions.Dto.PaymentTransactions;

public class NotRecognizedTransactionsRequest
{
    public DateTime PaymentDate { get; set; }
}