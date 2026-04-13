using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public class CurrencyPaymentToSupplierResponse : IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// Сумма в валюте
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма в рублях
        /// </summary>
        public decimal TotalSum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public KontragentWithRequisites Kontragent { get; set; }

        public bool TaxPostingsInManualMode { get; set; }

        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        /// <summary>
        /// Связанные инвойсы
        /// </summary>
        public RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> Documents { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsReadOnly { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public bool IsFromImport { get; set; }
    }
}