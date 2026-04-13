using System;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.Registration.Dto
{
    public class RegistrationKeyInfoDto
    {
        public Guid Key { get; set; }

        public int UserId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        
        /// <summary>
        /// Совпадение с ранее сохраненным паролем
        /// </summary>
        public bool IsPasswordSimilar { get; set; }

        public int FirmId { get; set; }

        public Tariff Tariff { get; set; }
    }
}