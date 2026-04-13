using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Requisites.Enums.FirmRequisites;

namespace Moedelo.Requisites.Kafka.Abstractions.Firm.Events
{
    public class RegistrationDataChanged : IEntityEventData
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }

        public bool? IsOoo { get; set; }
        public Opf? Opf { get; set; }

        public string Pseudonym { get; set; }
        public string ShortPseudonym { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int? MainActivityId { get; set; }
        public string MainActivityCode { get; set; }
        public string MainActivityName { get; set; }
        public string Okpo { get; set; }
        public string InFace { get; set; }
        public string InReason { get; set; }

        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}