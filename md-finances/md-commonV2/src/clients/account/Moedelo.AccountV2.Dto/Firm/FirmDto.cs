using System;

namespace Moedelo.AccountV2.Dto.Firm
{
    public class FirmDto : IFirmDto
    {
        public int Id { get; set; }

        public bool IsEnvd { get; set; }

        public bool IsUsn { get; set; }

        public DateTime? RegistrationInService { get; set; }

        public bool IsEmployerMode { get; set; }

        public bool IsOoo { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public int? LegalUserId { get; set; }

        public bool IsInternal { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Pseudonym { get; set; }

        public string ShortPseudonym { get; set; }

        public string Inn { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneCode { get; set; }
    }
}