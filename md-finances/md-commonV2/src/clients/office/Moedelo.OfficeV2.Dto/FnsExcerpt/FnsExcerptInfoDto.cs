using System;
using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.FnsExcerpt
{
    public class FnsExcerptInfoDto
    {
        public int OrgUid { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Ogrn { get; set; }

        public string Okpo { get; set; }

        public List<LeadershipInfoDto> LeadershipList { get; set; }

        public string ContactPhone { get; set; }

        public string Address { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public string TerminationDateStatus { get; set; }

        public string RegistrationPlace { get; set; }

        public MainOkvedInfoDto MainOkved { get; set; }

        public List<OkvedInfoDto> OkvedList { get; set; }

        public List<FounderInfoDto> FounderList { get; set; }

        public decimal? ShareCapitalAmount { get; set; }

        public List<LicenseInfoDto> LicenseList { get; set; }

        public bool IsLiquidated { get; set; }

        public string LiquidatedReason { get; set; }

        public bool IsOoo { get; set; }

        public int? UlAddressCount { get; set; }

        public string StatusId { get; set; }

        public string IfnsCode { get; set; }

        public DateTime? InventoryDate { get; set; }

        public string PfrNumber { get; set; }

        public string FssNumber { get; set; }

        public List<MassDirectorsAndFoundersShortInfoDto> IpMassDirectorsAndFounders { get; set; }
    }
}