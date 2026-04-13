using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.MiddlemanReports.Events
{
    public sealed class SalesMiddlemanReportUpdatedMessage
    {
        /// <summary>
        /// BaseId документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int KontragentId { get; set; }

        /// <summary>
        /// Идентификатор базового документа (DocumentBaseId) договора
        /// </summary>
        public long? ContractBaseId { get; set; }

        /// <summary>
        /// признак "подписан"
        /// </summary>
        public SignStatus SignStatus { get; set; }

        /// <summary>
        /// Проведён в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }
    }
}
