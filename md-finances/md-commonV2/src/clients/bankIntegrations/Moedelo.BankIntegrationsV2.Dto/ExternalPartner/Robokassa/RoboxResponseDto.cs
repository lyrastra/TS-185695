namespace Moedelo.BankIntegrationsV2.Dto.ExternalPartner.Robokassa
{
    public class RoboxResponseDto
    {
        /// <summary> Идентификатор учетной записи в сервисе "Моё дело" </summary>
        public IdentityInfoDto IdentityInfo { get; set; }

        /// <summary> Результат доставки </summary>
        public DeliveryResultDto DeliveryResult { get; set; }
    }
}
