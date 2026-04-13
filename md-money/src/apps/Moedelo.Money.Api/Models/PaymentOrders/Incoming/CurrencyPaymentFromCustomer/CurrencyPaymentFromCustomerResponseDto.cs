using System;
using System.Collections.Generic;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Enums;
using Newtonsoft.Json;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    /// <summary>
    /// Операция "Валютная оплата от покупателя"
    /// </summary>
    public class CurrencyPaymentFromCustomerResponseDto
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
        /// Сумма платежа в валюте
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Итоговая сумма платежа в рублях
        /// </summary>
        public decimal TotalSum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public NdsResponseDto Nds { get; set; }

        /// <summary>
        /// Признак: проведено в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode { get; set; }

        public OperationType OperationType => OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer;

        /// <summary>
        /// Только "для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }
        
        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public RemoteServiceResponseDto<IReadOnlyCollection<DocumentLinkDto>> Documents { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }

        /// <summary>
        /// Тип системы налогообложения
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }
    }
}