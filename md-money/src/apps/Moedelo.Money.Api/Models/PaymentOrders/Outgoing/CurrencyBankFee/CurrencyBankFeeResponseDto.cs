using System;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Enums;
using Newtonsoft.Json;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyBankFee
{
    public class CurrencyBankFeeResponseDto
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
        /// Идентификатор валютного расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }
        
        /// <summary>
        /// Сумма платежа в валюте
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Итог валютной операции в рублях
        /// </summary>
        public decimal TotalSum { get; set; }
        
        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType OperationType => OperationType.CurrencyBankFee;
        
        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode { get; set; }
        
        /// <summary>
        /// Только "для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }
    }
}