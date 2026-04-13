using System;
using System.Collections.Generic;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models
{
    public class BudgetaryPaymentDto
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }

        public KbkDto Kbk { get; set; }

        public BudgetaryPeriodDto Period { get; set; }

        public int? TradingObjectId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool TaxPostingsInManualMode { get; set; }

        public bool IsPaid { get; set; }

        public RemoteServiceResponseDto<IReadOnlyCollection<LinkedPurchaseCurrencyInvoiceDto>> CurrencyInvoices { get; set; }
    }
}