using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.FrameInfo
{
    /// <summary> Информация о пользователе со списками идентификаторов фирм для фрейма </summary>
    public class AccountFrameInfoForUserWithFirmIdsResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Логин </summary>
        public string Login { get; set; }

        /// <summary> ФИО </summary>
        public string Fio { get; set; }

        /// <summary> Телефон из регистрации </summary>
        public string PhoneFromRegistration { get; set; }

        /// <summary> Идентификаторы фирм, в которых состоит пользователь  </summary>
        public List<int> FirmIds { get; set; }
    }
}