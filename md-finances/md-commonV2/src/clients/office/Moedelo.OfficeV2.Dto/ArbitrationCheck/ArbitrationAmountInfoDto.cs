namespace Moedelo.OfficeV2.Dto.ArbitrationCheck
{
    public class ArbitrationAmountInfoDto
    {
        public long CaseCount { get; set; }

        public decimal Claimant12MonthAmount { get; set; }

        public long Claimant12MonthCaseCount { get; set; }

        public decimal Defendant12MonthAmount { get; set; }

        public long Defendant12MonthCaseCount { get; set; }

        public decimal ClaimantAllTimeAmount { get; set; }

        public long ClaimantAllTimeCaseCount { get; set; }

        public decimal DefendantAllTimeAmount { get; set; }

        public long DefendantAllTimeCaseCount { get; set; }

        public decimal ClaimantLast100Amount { get; set; }

        public long ClaimantLast100CaseCount { get; set; }

        public decimal DefendantLast100Amount { get; set; }

        public long DefendantLast100CaseCount { get; set; } 
    }
}