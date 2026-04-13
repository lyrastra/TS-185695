namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    /// <summary> Общий класс для сущности, обозначающей интеграцию пользователя с внешней системой </summary>
    public class IntegratedUserDto : IntegratedUserBaseDto
    {
        /// <summary> Логин пользователя в "Моём деле" </summary>
        public string Login { get; set; }

        /// <summary> ИНН </summary>
        public string Inn { get; set; }

        /// <summary> КПП </summary>
        public string Kpp { get; set; }

        /// <summary> Зарезервированный int </summary>
        public int? ReservedInt { get; set; }
    }
}
