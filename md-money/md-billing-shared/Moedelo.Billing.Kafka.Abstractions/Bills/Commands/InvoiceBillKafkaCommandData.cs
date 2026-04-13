using System;
using System.Collections.Generic;
using Moedelo.Billing.Shared.Enums;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.Bills.Commands
{
    /// <summary>
    /// Команда на выставление счёта
    /// </summary>
    public class InvoiceBillKafkaCommandData : IEntityCommandData
    {
        /// <summary>
        /// Идентификатор команды
        /// </summary>
        public Guid RequestGuid { get; set; }

        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Признак технического платежа 
        /// </summary>
        public bool IsTechnicalBill { get; set; }

        /// <summary>
        /// Метод платежа 
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Промокод 
        /// </summary>
        public string PromoCode { get; set; }

        /// <summary>
        /// Продуктовые услуги 
        /// </summary>
        public IReadOnlyCollection<ProductConfigurationData> ProductConfigurations { get; set; }

        public BillCreationSource CreationSource { get; set; }

        /// <summary>
        /// Адрес почты клиента для отправки счёта
        /// </summary>
        public string ClientNotificationEmail { get; set; }

        /// <summary>
        /// Признак допродажи 
        /// </summary>
        public bool IsCrossSelling { get; set; }

        public class ProductConfigurationData
        {
            public int Duration { get; set; }

            public string Code { get; set; }

            public IReadOnlyDictionary<string, ModifierData> ModifiersValues { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? EndDate { get; set; }
        }

        public class ModifierData
        {
            public string GradationName { get; set; }

            public decimal? GradationScaleValue { get; set; }
        }
    }
}