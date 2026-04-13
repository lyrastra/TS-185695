using System;

namespace Moedelo.Accounts.Abstractions.Dto.Firm
{
    public class FirmDto
    {
        public int Id { get; set; }

        public string Inn { get; set; }

        public bool IsEnvd { get; set; }

        public bool IsUsn { get; set; }

        /// <summary>
        /// Дата последней регистрации в сервисе
        /// (может меняться в процессе реактивации пользователя)
        /// </summary>
        public DateTime? RegistrationInService { get; set; }

        public bool IsEmployerMode { get; set; }

        public bool IsOoo { get; set; }

        /// <summary>
        /// Дата регистрации бизнеса
        /// </summary>
        public DateTime? RegistrationDate { get; set; }

        public int? LegalUserId { get; set; }

        public bool IsInternal { get; set; }
    }
}