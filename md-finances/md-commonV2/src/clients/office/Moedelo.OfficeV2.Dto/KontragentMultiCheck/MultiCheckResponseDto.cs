using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Aggregator;
using Moedelo.OfficeV2.Dto.ArbitrationCheck;
using Moedelo.OfficeV2.Dto.BankruptcyCheck;
using Moedelo.OfficeV2.Dto.Finance;
using Moedelo.OfficeV2.Dto.FnsExcerpt;
using Moedelo.OfficeV2.Dto.StateContracts;

namespace Moedelo.OfficeV2.Dto.KontragentMultiCheck
{
    public class MultiCheckResponseDto
    {
        public long RequestId { get; set; }

        public FnsExcerptInfoDto FnsExcerpt { get; set; }

        public GenProcCheckCountInfoDto GenProcCheckCount { get; set; }

        public List<GenProcCheckInfoDto> GenProcCheckList { get; set; } 

        public List<ArbitrationCaseInfoDto> ArbitrationCaseList { get; set; }

        public BankruptcyMessagesInfoDto OrganizationMessages { get; set; }

        public int? ExecutoryMessagesCount { get; set; }

        public ArbitrationAmountInfoDto ArbitrationAmount { get; set; }

        public FinanceInfoDto FinanceInfo { get; set; }

        public FinanceReportDto FinanceReport { get; set; }

        public StateContractsInfoDto StateContracts { get; set; }

        public MultiCheckRatingInfoDto Rating { get; set; }

        public bool IsFinished { get; set; }

        public bool OnControl { get; set; }

        public int? FinanceArchiveMaxYear { get; set; }
    }
}