using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.KbkNumbers;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Enums.Money.Operations.PaymentOrders;

namespace Moedelo.Finances.Domain.Models.Money.Operations.CashOrders
{
    public class CashOrderOperation
    {
        public long Id { get; set; }
        public int FirmId { get; set; }
        public long CashId { get; set; }
        public MoneyDirection Direction { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int? KontragentId { get; set; }
        public int? SalaryWorkerId { get; set; }
        public string Comments { get; set; }
        public string Destination { get; set; }
        public string DestinationName { get; set; }
        public decimal Sum { get; set; }
        public long AccountingDocumentId { get; set; }
        public bool IsProvideInAccounting { get; set; }
        public long? AssociatedCashOrderId { get; set; }

        #region postings
        public ProvidePostingType PostingsAndTaxMode { get; set; }
        public ProvidePostingType TaxPostingType { get; set; }
        public TaxationSystemType TaxationSystemType { get; set; }
        #endregion
        
        public string GivePerson { get; set; }
        public long? DestinationCashId { get; set; }
        public int? SetlementAccountId { get; set; } //TODO SettlementAccountId?
        public decimal? PaidCardSum { get; set; }

        #region nds 
        public bool IncludeNds { get; set; }
        public PaymentOrderNdsType NdsType { get; set; } //TODO CashOrderNdsType? или PaymentOrderNdsType => OrderNdsType
        public decimal? NdsSum { get; set; }
        #endregion

        public OperationType OperationType { get; set; }
        public long? AccountingOperationId { get; set; }
        public int? WorkerDocumentType { get; set; }
        public int? ReturnType { get; set; }
        public string ProjectNumber { get; set; }
        public bool IsChargedNdsByAdvance { get; set; }
        public bool IsNominateInvoice { get; set; }
        public long? MemorialWarrantId { get; set; }
        public string SingleReasonDocumentName { get; set; }
        public long DocumentBaseId { get; set; }
        public long? SyntheticAccountTypeId { get; set; }
        public string PaybillNumber { get; set; }
        public DateTime? PaybillDate { get; set; }
        public string BillNumber { get; set; }
        public string ZReportNumber { get; set; }

        #region loans
        /// <summary>Признак долгосрочного займа</summary>
        public bool? IsLongTermLoan { get; set; }
        /// <summary>Сумма процентов по займу</summary>
        public decimal? LoanInterestSum { get; set; }
        #endregion

        #region mediation
        /// <summary>Признак посредничества</summary>
        public bool? IsMediation { get; set; }
        /// <summary>Вознаграждение посредника</summary>
        public decimal? MediationCommission { get; set; }
        #endregion

        #region budgetary payment
        public int? BudgetaryTaxesAndFees { get; set; }
        public BudgetaryPeriodType? BudgetaryPeriodType { get; set; }
        public int? BudgetaryPeriodNumber { get; set; }
        public int? BudgetaryPeriodYear { get; set; }
        public int? KbkId { get; set; }
        public string KbkNumber { get; set; }
        public KbkNumberType? KbkType { get; set; }
        public KbkPaymentType? KbkPaymentType { get; set; }
        public bool IsPayment { get; set; }
        public int? PatentId { get; set; }
        #endregion

        public DateTime ModifyDate { get; set; }
        public DateTime CreateDate { get; set; }
        
        public DateTime? BudgetaryPeriodDate { get; set; }
        
        public List<UnifiedBudgetarySubPayment> SubPayments { get; set; } = new List<UnifiedBudgetarySubPayment>();
    }
}
