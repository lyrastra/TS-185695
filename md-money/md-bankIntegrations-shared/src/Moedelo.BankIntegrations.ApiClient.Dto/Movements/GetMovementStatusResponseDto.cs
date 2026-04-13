namespace Moedelo.BankIntegrations.Dto.Movements
{
    public class GetMovementStatusResponseDto : BaseResponseDto
    {
        /// <summary>
        /// TODO: переделать, значения из енама md-enums Moedelo.Common.Enums.Enums.Integration.IntegrationRequestPartStatus
        /// </summary>
        public int IntegrationRequestPartStatusId { get; set; }
        /// <summary>
        /// Уникальный идентификатор запроса выписки у банка-партнера
        /// </summary>
        public string ExternalRequestId { get; set; }
    }
}
