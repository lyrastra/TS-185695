using System;
using Moedelo.BankIntegrationsV2.Dto.Integrations;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class RequestMovementListRequestDto
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsManual { get; set; }
        public bool IsAccounting { get; set; }
        public IntegrationIdentityDto IdentityDto { get; set; }

        /// <summary> Тип запроса </summary>
        public IntegrationCallType? CallType { get; set; }

        /// <summary> Почта для отправки отчёта о сверке для партнерки </summary>
        public string Email { get; set; }

        /// <summary> Идентификатор сесии для сверки для пользователя </summary>
        public Guid Guid { get; set; }
    }
}
