using System.Collections.Generic;

namespace Moedelo.BackofficeV2.Dto.FrameInfo
{
    /// <summary> Информация о пользователе для фрейма </summary>
    public class FrameInfoForUserResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Логин </summary>
        public string Login { get; set; }

        /// <summary> ФИО </summary>
        public string Fio { get; set; }

        /// <summary> Телефон из регистрации </summary>
        public string PhoneFromRegistration { get; set; }

        /// <summary> Список фирм, в которых состоит пользователь </summary>
        public List<FrameInfoForFirmResponseDto> Firms { get; set; }
    }
}