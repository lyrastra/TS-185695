using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn
{
    public class LoanReturnResponse : IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public decimal? LoanInterestSum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public KontragentWithRequisites Kontragent { get; set; }

        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        public bool IsLongTermLoan { get; set; }

        public bool TaxPostingsInManualMode { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsReadOnly { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public bool IsFromImport { get; set; }
    }
}
