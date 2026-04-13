using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Dto
{
    public class RefundToCustomerPaymentDto
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public ContractDto Kontragent { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public bool IsMainContractor { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool TaxPostingsInManualMode { get; set; }

        public RemoteServiceResponseDto<ContractDto> Contract { get; set; }

        public bool IsPaid { get; set; }

        public bool IsReadOnly { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }
    }
}