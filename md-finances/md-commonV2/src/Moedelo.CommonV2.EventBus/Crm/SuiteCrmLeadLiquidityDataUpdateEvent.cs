using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmLeadLiquidityDataUpdateEvent
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public string FirmId { get; set; }

        public Opportunity[] Opportunities { get; set; }

        /// <summary>
        ///  Идентификатор пользователя-автора изменений в SuiteCrm
        /// </summary>
        public string ModifiedBy { get; set; }

        public class Opportunity
        {
            /// <summary>
            /// Идентификатор сделки
            /// </summary>
            public string OpportunityId { get; set; }

            public DateTime? DateEntered { get; set; }

            public DateTime? DateModified { get; set; }

            public string SalesStage { get; set; }

            /// <summary>
            /// Причина отказа
            /// </summary>
            public string DeclineCase { get; set; }

            /// <summary>
            /// Информирован банком
            /// </summary>
            public string InfoByBankWorker { get; set; }

            /// <summary>
            /// Дата звонка
            /// </summary>
            public DateTime? NextTimeCall { get; set; }

            /// <summary>
            /// Изменилось поле Дата звонка
            /// </summary>
            public bool NextTimeCallChanged { get; set; }

            /// <summary>
            /// История изменений поля Дата звонка
            /// </summary>
            public IReadOnlyCollection<ValueHistoryItem<DateTime?>> NextTimeCallHistory { get; set; }
        }

        public class ValueHistoryItem<T>
        {
            /// <summary>
            /// Дата изменений
            /// </summary>
            public DateTime Changed { get; set; }

            public T Value { get; set; }
        }
    }
}