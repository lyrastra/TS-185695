using System;
using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Events
{
    /// <summary>
    /// Общие данные для событий создания/изменения авансового отчёта
    /// </summary>
    public class BaseAdvanceStatementMessage : IEntityEventData
    {
        /// <summary>
        /// Идентификатор авансового отчёта
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата авансового отчета
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// Связанные списания "выдача подотчетному лицу" (аванс и перерасход)
        /// </summary>
        public IReadOnlyCollection<OutgoingPayment> OutgoingPayments { get; set; }

        /// <summary>
        /// Проведён в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }
    }
}