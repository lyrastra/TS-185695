using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Enums;
using Newtonsoft.Json;
using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.RefundToCustomer
{
    /// <summary>
    /// Операция "Возврат покупателю"
    /// </summary>
    public class RefundToCustomerResponseDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public ContractorResponseDto Contractor { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public RemoteServiceResponseDto<ContractDto> Contract { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public NdsResponseDto Nds { get; set; }

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        public bool IsMainContractor { get; set; }

        /// <summary>
        /// Признак: проведено в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode { get; set; }

        public OperationType OperationType => OperationType.PaymentOrderOutgoingRefundToCustomer;

        /// <summary>
        /// Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Только "для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }
    }
}
