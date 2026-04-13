using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public class CurrencyPaymentToSupplierSaveRequest : IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// Сумма платежа в валюте
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма платежа в рублях
        /// </summary>
        public decimal TotalSum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public KontragentWithRequisites Kontragent { get; set; }

        public long? ContractBaseId { get; set; }

        public TaxPostingsData TaxPostings { get; set; }

        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Связанные инвойсы
        /// </summary>
        public IReadOnlyCollection<DocumentLinkSaveRequest> DocumentLinks { get; set; }

        /// <summary>
        /// Старые связанные инвойсы. Нужны для НУ
        /// </summary>
        public IReadOnlyCollection<DocumentLinkSaveRequest> OldDocumentLinks { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public string SourceFileId { get; set; }

        public bool IsSaveNumeration { get; set; }

        /// <summary>
        /// Идентификаторы применённых правил импорта
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        /// <summary>
        /// Идентификатор лога импорта
        /// </summary>
        public int? ImportLogId { get; set; }
    }
}