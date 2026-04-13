using System;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction
{
    public class DeductionResponse : IActualizableReadResponse, IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public KontragentWithRequisites Contractor { get; set; }

        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        public bool IsPaid { get; set; }

        public bool IsReadOnly { get; set; }

        public bool IsFromImport { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public string Kbk { get; set; }

        public string Oktmo { get; set; }

        public string Uin { get; set; }

        public string DeductionWorkerDocumentNumber { get; set; }

        public PaymentPriority PaymentPriority { get; set; }

        public int? DeductionWorkerId { get; set; }

        public string DeductionWorkerInn { get; set; }

        public string DeductionWorkerFio { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsBudgetaryDebt { get; set; }
        
        public BudgetaryPayerStatus PayerStatus { get; set; }
    }
}