using System;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RequisitesV2.Dto.FirmRequisites
{
    public class RegistrationDataDto
    {
        public bool IsOoo { get; set; }
        public Opf Opf { get; set; }

        public string Pseudonym { get; set; }
        public string ShortPseudonym { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Okpo { get; set; }

        public int? MainActivityId { get; set; }
        public string MainActivityCode { get; set; }
        public string MainActivityName { get; set; }

        public string InFace { get; set; }
        public string InReason { get; set; }

        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}