using System;
using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.ArbitrationCheck
{
    public class ArbitrationCaseInfoDto
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public string Category { get; set; }

        public string Stage { get; set; }

        public string Type { get; set; }

        public string TypeMCode { get; set; }

        public string ClaimSum { get; set; }

        public DateTime? CloseDate { get; set; }

        public string CourtName { get; set; }

        public bool IsSimpleJustice { get; set; }

        public DateTime LastDocDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<ArbitrationCaseSideInfoDto> ClaimantList { get; set; }

        public List<ArbitrationCaseSideInfoDto> DefendantList { get; set; }

        public List<ArbitrationCaseSideInfoDto> ThirdPartyList { get; set; }

        public string LastDocCourt { get; set; }

        public string LastDocDescription { get; set; }

        public string Decision { get; set; }
    }
}