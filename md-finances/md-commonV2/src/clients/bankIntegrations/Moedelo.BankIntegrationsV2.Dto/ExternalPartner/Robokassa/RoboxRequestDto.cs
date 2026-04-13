namespace Moedelo.BankIntegrationsV2.Dto.ExternalPartner.Robokassa
{
    /// <summary> Запрос к сервису "Моё дело" на передачу выписок </summary>
    public class RoboxRequestDto
    {
        /// <summary> Пользователь, которому адресовано сообщение </summary>
        public IdentityInfoDto IdentityInfo { get; set; }

        /// <summary> Данные операций по магазинам </summary>
        public string RoboxBillingText { get; set; }
    }
}
