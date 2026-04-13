using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing
{
    public class DeductionDto
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }
        public int SettlementAccountId { get; set; }
        public string Description { get; set; }
        public KontragentWithRequisitesDto Contractor { get; set; }

        public OperationState OperationState { get; set; }
        public string SourceFileId { get; set; }
        public long? DuplicateId { get; set; }
        public OutsourceState? OutsourceState { get; set; }

        public bool IsPaid { get; set; }
        public bool IsBudgetaryDebt { get; set; }
        public PaymentPriority PaymentPriority { get; set; }
        public string Oktmo { get; set; }
        public string Kbk { get; set; }
        public string Uin { get; set; }
        public string DeductionWorkerDocumentNumber { get; set; }
        public int? DeductionWorkerId { get; set; }
        public string DeductionWorkerInn { get; set; }
        public bool ProvideInAccounting { get; set; }
        public BudgetaryPayerStatus PayerStatus { get; set; }
    }
}