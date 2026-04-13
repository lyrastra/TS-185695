using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentDto
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }
        public int SettlementAccountId { get; set; }
        public string Description { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }
        public BudgetaryPaymentType PaymentType { get; set; }
        public KbkPaymentType KbkPaymentType { get; set; }
        public int? KbkId { get; set; }
        public string KbkNumber { get; set; }
        public BudgetaryPeriodDto Period { get; set; }
        public BudgetaryPayerStatus PayerStatus { get; set; }
        public BudgetaryPaymentBase PaymentBase { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentNumber { get; set; }
        public BudgetaryRecipientRequisitesDto Recipient { get; set; }
        public string Uin { get; set; }

        public OperationState OperationState { get; set; }
        public string SourceFileId { get; set; }
        public long? DuplicateId { get; set; }
        public OutsourceState? OutsourceState { get; set; }

        public bool ProvideInAccounting { get; set; }
        public ProvidePostingType PostingsAndTaxMode { get; set; }
        public ProvidePostingType TaxPostingType { get; set; }
        public bool IsPaid { get; set; }
        public int? TradingObjectId { get; set; }
        
        public TaxationSystemType? TaxationSystemType { get; set; }
        public long? PatentId { get; set; }
    }
}
