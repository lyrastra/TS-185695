using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.Other
{
    public class OtherOutgoingResponse : IActualizableReadResponse, IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public ContractorWithRequisites Contractor { get; set; }

        public bool ProvideInAccounting { get; set; }

        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public bool IsPaid { get; set; }

        public bool IsReadOnly { get; set; }

        public bool IsFromImport { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}
