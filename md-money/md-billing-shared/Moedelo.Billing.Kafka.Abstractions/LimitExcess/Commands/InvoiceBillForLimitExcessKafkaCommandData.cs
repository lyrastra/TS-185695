using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.LimitExcess.Commands
{
    /// <summary>
    /// Команда на выставление счёта за превышение лимита
    /// </summary>
    public class InvoiceBillForLimitExcessKafkaCommandData : IEntityCommandData
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }
        
        /// <summary>
        /// Дата начала периода превышения
        /// Используется как дата начала действия счёта
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Дата окончания периода превышения
        /// Используется как дата окончания действия счёта
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Технический код лимита
        /// </summary>
        public string LimitCode { get; set; }
        
        /// <summary>
        /// Текущее значение лимита
        /// </summary>
        public int? CurrentLimitValue { get; set; }
        
        /// <summary>
        /// Значение превышения лимита
        /// </summary>
        public int LimitExcessValue { get; set; }
        
        /// <summary>
        /// Адрес почты клиента для отправки счёта
        /// </summary>
        public string ClientNotificationEmail { get; set; }
    }
}