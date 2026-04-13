using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.AccountV2.Dto.ProfOutsource
{
    public class FirmInfoDto
    {
        public int Id { get; set; }

        public string AdminName { get; set; }

        public string AdminSurname { get; set; }

        public string AdminPatronymic { get; set; }

        public LegalType LegalType { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Inn { get; set; }

        public string Phone { get; set; }

        public string TariffName { get; set; }
    }
}