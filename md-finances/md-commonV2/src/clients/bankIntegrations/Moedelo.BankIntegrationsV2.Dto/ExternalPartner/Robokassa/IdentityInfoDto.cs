namespace Moedelo.BankIntegrationsV2.Dto.ExternalPartner.Robokassa
{
    /// <summary> Идентификация пользователя </summary>
    public class IdentityInfoDto
    {
        /// <summary> Логин пользователя в сервисе "Моё дело" </summary>
        public string Login { get; set; }

        /// <summary> ИНН клиента </summary>
        public string Inn { get; set; }
    }
}
