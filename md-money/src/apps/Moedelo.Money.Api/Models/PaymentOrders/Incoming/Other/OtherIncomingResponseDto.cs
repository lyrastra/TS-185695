using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.Other
{
    /// <summary>
    /// Операция "Прочее поступление"
    /// </summary>
    public class OtherIncomingResponseDto
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
        /// Контрагент/Сотрудник
        /// </summary>
        public ContractorResponseDto Contractor { get; set; }

        /// <summary>
        /// Тип контрагента
        /// </summary>
        public ContractorType ContractorType { get; set; }

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
        /// Признак: проведено в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Связанные счета
        /// </summary>
        public RemoteServiceResponseDto<IReadOnlyCollection<BillLinkDto>> Bills { get; set; }

        public OperationType OperationType => OperationType.PaymentOrderIncomingOther;

        /// <summary>
        /// Только "для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode => true;

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public NdsResponseDto Nds { get; set; }

        /// <summary>
        /// Тип системы налогообложения
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Идентификатор привязанного патента
        /// </summary>
        public long? PatentId { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }

        /// <summary>
        /// Не удаляемая атоматически
        /// </summary>
        public bool NoAutoDeleteOperation { get; set; }

        /// <summary>
        /// Целевое поступление
        /// </summary>
        public bool IsTargetIncome { get; set; }
    }
}
