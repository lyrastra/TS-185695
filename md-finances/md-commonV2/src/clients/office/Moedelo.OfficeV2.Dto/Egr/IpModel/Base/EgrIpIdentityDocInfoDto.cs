using System;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Base
{
    public class EgrIpIdentityDocInfoDto : EgrIpGrnIpBaseDto
    {
        public string DocTypeCode { get; set; }

        public string DocName { get; set; }

        public string SeriesNumber { get; set; }

        public DateTime IssueDate { get; set; }

        public bool IssueDateSpecified { get; set; }

        public string IssueBy { get; set; }

        public string DivisionCode { get; set; }
    }
}