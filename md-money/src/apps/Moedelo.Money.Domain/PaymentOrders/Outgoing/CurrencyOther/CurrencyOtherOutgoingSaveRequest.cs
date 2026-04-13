using System;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther
{
    public class CurrencyOtherOutgoingSaveRequest : IPaymentOrderOutsourceSaveRequest
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

        public int SettlementAccountId { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public ContractorWithRequisites Contractor { get; set; }

        public long? ContractBaseId { get; set; }

        public TaxPostingsData TaxPostings { get; set; }
        
        public bool ProvideInAccounting { get; set; }
        
        public CurrencyOtherOutgoingCustomAccPosting AccPosting { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public bool IsSaveNumeration { get; set; }

    }
}