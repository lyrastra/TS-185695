using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.CommonV2.EventBus.Integrations
{
    public class MovementListReviseEvent
    {
        public string MongoObjectId { get; set; }

        /// <summary> Почта, на которую отправится отчёт </summary>
        public string Email { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        /// <summary> Логин клиента </summary>
        public string Login { get; set; }

        /// <summary> Р/сч клиента </summary>
        public string SettlementNumber { get; set; }

        /// <summary> Фирма клиента </summary>
        public int FirmId { get; set; }

        public MovementReviseStatus Status { get; set; }
    }
}