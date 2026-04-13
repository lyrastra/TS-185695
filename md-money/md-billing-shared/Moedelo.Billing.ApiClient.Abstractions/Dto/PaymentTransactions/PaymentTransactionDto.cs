using System;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.PaymentTransactions;

public class PaymentTransactionDto
{
    public int Id { get; set; }

    public int? PaymentHistoryId { get; set; }

    public PaymentTransactionType Type { get; set; }

    public decimal Sum { get; set; }

    public DateTime Date { get; set; }

    public string TransactionNumber { get; set; }

    public string PayerInn { get; set; }

    public string PayerKpp { get; set; }

    public string PayerName { get; set; }

    /// <summary>
    /// р/сч на который переведены деньги
    /// </summary>
    public string SellerSettlementAccount { get; set; }

    public int? PartnerId { get; set; }

    public string PartnerName { get; set; }

    public string PayerBik { get; set; }

    public string PayerSettlementAccount { get; set; }
    
    public RegionalPartnerType PartnerType { get; set; }
}