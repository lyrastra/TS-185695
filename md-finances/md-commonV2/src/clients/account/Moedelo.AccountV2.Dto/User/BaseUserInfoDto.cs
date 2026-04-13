using System;

namespace Moedelo.AccountV2.Dto.User
{
    public class BaseUserInfoDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public string Login { get; set; }

        public string Fio { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string Phone { get; set; }
    }
}