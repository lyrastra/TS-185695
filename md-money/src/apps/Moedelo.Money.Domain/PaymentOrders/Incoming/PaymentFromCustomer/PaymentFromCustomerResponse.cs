using System;
using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer
{
    public class PaymentFromCustomerResponse : IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public KontragentWithRequisites Kontragent { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public bool IncludeNds { get; set; }
        public NdsType? NdsType { get; set; }
        public decimal? NdsSum { get; set; }

        /// <summary>
        /// НДС для посредничества
        /// </summary>
        public NdsType? MediationNdsType { get; set; }
        public decimal? MediationNdsSum { get; set; }

        public bool IsMediation { get; set; }

        public decimal? MediationCommissionSum { get; set; }

        public bool IsMainContractor { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool TaxPostingsInManualMode { get; set; }

        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<BillLink>> Bills { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> Documents { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<InvoiceLink>> Invoices { get; set; }

        public RemoteServiceResponse<decimal?> ReserveSum { get; set; }

        public bool IsReadOnly { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public bool IsFromImport { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}
