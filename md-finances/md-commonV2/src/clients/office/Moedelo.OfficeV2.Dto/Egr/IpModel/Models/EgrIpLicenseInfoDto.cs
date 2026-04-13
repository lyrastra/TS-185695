using System;
using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpLicenseInfoDto : EgrIpGrnIpBaseDto
    {
        public List<string> TypeName { get; set; }

        public List<string> PlaceActivity { get; set; }

        public string Licensor { get; set; }

        public EgrIpLicenseSuspensionInfoDto LicenseSuspension { get; set; }

        public string Series { get; set; }

        public string Number { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool EndDateSpecified { get; set; }
    }
}