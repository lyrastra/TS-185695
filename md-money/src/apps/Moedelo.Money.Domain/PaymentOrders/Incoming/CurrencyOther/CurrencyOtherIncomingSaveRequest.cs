using Moedelo.Money.Domain.TaxPostings;
using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther
{
    public class CurrencyOtherIncomingSaveRequest : IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// В валюте
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// В рублях
        /// </summary>
        public decimal TotalSum { get; set; }

        public string Description { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public int SettlementAccountId { get; set; }

        public ContractorWithRequisites Contractor { get; set; }

        public long? ContractBaseId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public TaxPostingsData TaxPostings { get; set; }

        public CurrencyOtherIncomingCustomAccPosting AccPosting { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}