using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Kontragents.Arbitration;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Arbitration
{
    /// <summary>
    /// Арбитражное дело
    /// </summary>
    public class CaseInfoDto
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public string Category { get; set; }

        public string Stage { get; set; }

        public string Type { get; set; }

        public string TypeMCode { get; set; }

        public decimal ClaimSum { get; set; }

        public DateTime? CloseDate { get; set; }

        public string CourtName { get; set; }

        public bool IsSimpleJustice { get; set; }

        public DateTime LastDocDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<CaseSideInfoDto> ClaimantList { get; set; }

        public List<CaseSideInfoDto> DefendantList { get; set; }

        public List<CaseSideInfoDto> ThirdPartyList { get; set; }

        public string LastDocCourt { get; set; }

        public string LastDocDescription { get; set; }

        public ProcessParticipantEnum[] ProcessParticipantTypes { get; set; }

        public string Decision { get; set; }
        
        public string DecisionCourt { get; set; }

        public CaseBankruptcyDto Bankruptcy { get; set; }

        public CaseResultCode? ResultCode { get; set; }
        
        public string NextSession { get; set; }
        
        public CaseStateEnumDto? CaseState { get; set; }
    }
}