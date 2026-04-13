using System;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Enums;
using Newtonsoft.Json;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyPurchase
{
    public class IncomingCurrencyPurchaseDto
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
        /// Идентификатор рублевого расчётного счёта (с которого производится списание)
        /// </summary>
        public int FromSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в валюте
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType OperationType => OperationType.PaymentOrderIncomingCurrencyPurchase;

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }
        
        /// <summary>
        /// Только "для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }
    }
}