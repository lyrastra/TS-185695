using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        /// <summary>
        /// Для ui не используется, возможно понадобится для генерации бланков п/п
        /// </summary>
        public BudgetaryPaymentType PaymentType { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }

        public int? KbkId { get; set; }

        public string KbkNumber { get; set; }

        public BudgetaryPeriod Period { get; set; }

        public BudgetaryPayerStatus PayerStatus { get; set; }

        public BudgetaryPaymentBase PaymentBase { get; set; }

        public string DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public string Uin { get; set; }

        public BudgetaryRecipient Recipient { get; set; }

        public int? TradingObjectId { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }
        
        public long? PatentId { get; set; }

        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}
