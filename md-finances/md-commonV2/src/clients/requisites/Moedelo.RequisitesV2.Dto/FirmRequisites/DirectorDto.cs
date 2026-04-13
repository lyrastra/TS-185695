using System;

namespace Moedelo.RequisitesV2.Dto.FirmRequisites
{
    public class DirectorDto
    {
        public int WorkerId { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string PassportSerialNumber { get; set; }
        public string PassportNumber { get; set; }
        public string PassportAdd { get; set; }
        public DateTime? PassportDate { get; set; }
        public string PassportOfficeCode { get; set; }

        public string Snils { get; set; }

        public DateTime? DateOfStartWork { get; set; }

        public bool IsForeigner { get; set; }
        public bool? IsMale { get; set; }
        public string CountryCode { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Inn { get; set; }
    }
}