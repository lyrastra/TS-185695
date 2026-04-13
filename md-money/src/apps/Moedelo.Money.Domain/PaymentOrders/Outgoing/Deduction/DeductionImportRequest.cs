using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction
{
    public class DeductionImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public KontragentWithRequisites Contractor { get; set; }

        public long? ContractBaseId { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int[] ImportRuleIds { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
        
        public string PayerStatus { get; set; }
        public string Kbk { get; set; }
        public string Oktmo { get; set; }
        public string Uin { get; set; }
        public string DeductionWorkerDocumentNumber { get; set; }
        public bool IsBudgetaryDebt { get; set; }
        public int? DeductionWorkerId { get; set; }
        public string DeductionWorkerInn { get; set; }
        public PaymentPriority PaymentPriority { get; set; }

        public int? ImportLogId { get; set; }
    }
}
