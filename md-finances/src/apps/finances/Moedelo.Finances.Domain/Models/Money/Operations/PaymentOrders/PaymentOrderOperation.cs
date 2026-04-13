using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.KbkNumbers;
using Moedelo.Common.Enums.Enums.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Finances.Domain.Enums.Money.Operations.PaymentOrders;

namespace Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders
{
    public class PaymentOrderOperation
    {
        public long Id { get; set; }
        public int FirmId { get; set; }
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }
        public string Number { get; set; }
        public Enums.Money.MoneyDirection Direction { get; set; }
        public OperationType OperationType { get; set; }
        public OperationState OperationState { get; set; }
        public DocumentStatus PaidStatus { get; set; }
        public decimal Sum { get; set; }
        public string Description { get; set; }
        public PaymentPriority PaymentPriority { get; set; }
        public string PaymentSnapshot { get; set; }

        public int? SettlementAccountId { get; set; }
        public int? TransferSettlementAccountId { get; set; }

        public int? KontragentId { get; set; }
        public string KontragentName { get; set; }

        public int? WorkerId { get; set; }
        public PaymentOrderWorkerReasonDocumentType UnderContract { get; set; }

        public BankDocType OrderType { get; set; }

        /// <summary> nds </summary>
        public bool IncludeNds { get; set; }
        public PaymentOrderNdsType NdsType { get; set; }
        public decimal? NdsSum { get; set; }

        #region budgetary payment

        public int? BudgetaryTaxesAndFees { get; set; }
        public BudgetaryPeriodType? BudgetaryPeriodType { get; set; }
        public int? BudgetaryPeriodNumber { get; set; }
        public int? BudgetaryPeriodYear { get; set; }
        public DateTime? BudgetaryPeriodDate { get; set; }
        public KbkPaymentType? KbkPaymentType { get; set; }
        public int? KbkId { get; set; }
        public string KbkNumber { get; set; }
        public KbkNumberType? KbkType { get; set; }
        public int? TradingObjectId { get; set; }
        public int? PatentId { get; set; }

        #endregion

        /// <summary> for import </summary>
        public string SourceFileId { get; set; }

        public List<UnifiedBudgetarySubPayment> SubPayments { get; set; } = new List<UnifiedBudgetarySubPayment>();
    }
}