using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee
{
    public class CurrencyBankFeeImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public decimal TotalSum { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }
        
    }
}