using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.CommonV2.EventBus.Integrations
{
    public class MovementListReviseForUserEvent
    {
        public string MongoObjectId { get; set; }

        /// <summary> Идентификатор сессии </summary>
        public Guid Guid { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        /// <summary> Р/сч клиента </summary>
        public string SettlementNumber { get; set; }

        /// <summary> Фирма клиента </summary>
        public int FirmId { get; set; }

        public MovementReviseStatus Status { get; set; }

        public bool IsManual { get; set; }
    }
}