using System;

namespace Moedelo.Accounts.Abstractions.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public int? AccountId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Fio { get; set; }

        public string RegistrationGuid { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string Phone { get; set; }

        public string UtmSource { get; set; }
    }
}