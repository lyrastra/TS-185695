using System;

namespace Moedelo.RequisitesV2.Dto.FirmRequisites
{
    public class FirmDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Fio { get; set; }
        public bool IsOoo { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Pfr { get; set; }
        public string PfrEmployer { get; set; }
        public string PfrCode { get; set; }
        public string Snils { get; set; }
        public string Tfoms { get; set; }
        public bool IsEmployerMode { get; set; }
        public int? TaxAdministrationId { get; set; }
        public string TaxAdministrationInn { get; set; }
        public int? PfrDetailsId { get; set; }
        public int? FssDisabilityDetailsId { get; set; }
        public int? FssInjuryDetailsId { get; set; }
        public int? RequisitesId { get; set; }
        public string InFace { get; set; }
        public string InReason { get; set; }
        public string KladrCode { get; set; }
        public int? KladrVersion { get; set; }
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
        public bool IsManualCashMode { get; set; }
        public DateTime? RegistrationInService { get; set; }
        public string PlaceOfBirth { get; set; }
        public string CountryCode { get; set; }
        public string Sex { get; set; }
        public string FssNumber { get; set; }
        public string SubordinationCode { get; set; }
        public string FssAddress { get; set; }
        public string FssDepartment { get; set; }
        public string IpSurname { get; set; }
        public string IpName { get; set; }
        public string IpPatronymic { get; set; }
        public bool NeedAccountant { get; set; }
        public int PartnerId { get; set; }
        public string Okopf { get; set; }
        public string Okfs { get; set; }
    }
}
