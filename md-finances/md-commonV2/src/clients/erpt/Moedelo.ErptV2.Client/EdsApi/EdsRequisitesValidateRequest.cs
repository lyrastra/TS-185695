using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.EdsRequest;

namespace Moedelo.ErptV2.Client.EdsApi
{
    public class EdsRequisitesValidateRequest
    {
        public int FirmId { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string FioDirector { get; set; }
        public string VacancyDirector { get; set; }
        public string Address { get; set; }
        public string PassportSerial { get; set; }
        public string PassportNumber { get; set; }
        public string PfrNumberForEmployes { get; set; }
        public string PfrCode { get; set; }
        public string FssNumber { get; set; }
        public string FssSubordination { get; set; }
        public string FsgsCode { get; set; }
        public string FnsCode { get; set; }
        public bool IsOoo { get; set; }
        public string Sex { get; set; }
        public List<EdsValidateRequisite> WithoutValidateRequisites { get; set; }
    }
}