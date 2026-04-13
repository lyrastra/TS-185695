using Moedelo.BankIntegrations.Models.Movement;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.Dto.Movements
{
    public class RequestMovementResponseDto : BaseResponseDto
    {
        /// <summary> Идентификатор запроса из внешней системы </summary>
        public string ExternalRequestId { get; set; }

        public MDMovementList MDMovementList { get; set; }

        public IntegrationErrorType ErrorType { get; set; }
    }
}
