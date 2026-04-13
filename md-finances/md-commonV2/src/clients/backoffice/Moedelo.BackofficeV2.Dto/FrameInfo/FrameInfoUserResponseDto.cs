namespace Moedelo.BackofficeV2.Dto.FrameInfo
{
    /// <summary> Информация о пользователе в фирме для фрейма </summary>
    public class FrameInfoUserResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Логин </summary>
        public string Login { get; set; }

        /// <summary> ФИО </summary>
        public string Fio { get; set; }

        /// <summary> Телефон из регистрации </summary>
        public string PhoneFromRegistration { get; set; }

        /// <summary> Роль </summary>
        public string Role { get; set; }
    }
}