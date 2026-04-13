using System.Collections.Generic;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CommissionAgentReports.Messages
{
    public interface ICommissionAgentReportMessage
    {
        /// <summary>
        /// Базовый идентификатор документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Передана только ЧАСТЬ ДАННЫХ! Модель обогатить через из API в случае необходимости.
        /// </summary>
        public bool IsTruncated { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<CommissionAgentReportItemMessage> Items { get; set; }
    }
}