using System;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class FirmRequisitesDto
    {
        public int Id { get; set; } // firmId
        public int? RequisitesId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Fio { get; set; }
        public bool IsOoo { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Snils { get; set; }
        public bool IsEmployerMode { get; set; }
        public string InFace { get; set; }
        public string InReason { get; set; }
        public string KladrCode { get; set; }
        public string House { get; set; }
        public string HouseName { get; set; }
        public string Building { get; set; }
        public string BuildingName { get; set; }
        public string Flat { get; set; }
        public string FlatName { get; set; }
        public string PostIndex { get; set; }
        public string Oktmo { get; set; }
        public string Okato { get; set; }
        public string Egrn { get; set; }
        public int DirectorId { get; set; }
        public int AccountantId { get; set; }
        public DateTime? RegistrationInService { get; set; }
        public string PlaceOfBirth { get; set; }
        public string CountryCode { get; set; }
        public string Sex { get; set; }
        public string IpSurname { get; set; }
        public string IpName { get; set; }
        public string IpPatronymic { get; set; }
        public string Pfr { get; set; }
        public string PfrEmployer { get; set; }
        public string FssNumber { get; set; }
        public string Tfoms { get; set; }
    }
}
