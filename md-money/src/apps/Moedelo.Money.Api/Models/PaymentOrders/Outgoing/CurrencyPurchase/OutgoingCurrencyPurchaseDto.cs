using System;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Enums;
using Newtonsoft.Json;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyPurchase
{
    public class OutgoingCurrencyPurchaseDto
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
        /// Идентификатор рублевого расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }
        
        /// <summary>
        /// Идентификатор валютного расчётного счёта
        /// </summary>
        public int? ToSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в рублях
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
        /// Курс валюты на дату документа
        /// </summary>
        public decimal ExchangeRate { get; set; }
        
        /// <summary>
        /// Курсовая разница от курса ЦБ 
        /// </summary>
        public decimal ExchangeRateDiff { get; set; }
        
        /// <summary>
        /// Итог валютной операции в валюте
        /// </summary>
        public decimal TotalSum { get; set; }
        
        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType OperationType => OperationType.PaymentOrderOutgoingCurrencyPurchase;
        
        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }

        /// <summary>
        /// Признак: только чтение
        /// </summary>
        public bool IsReadOnly { get; set; }
    }
}