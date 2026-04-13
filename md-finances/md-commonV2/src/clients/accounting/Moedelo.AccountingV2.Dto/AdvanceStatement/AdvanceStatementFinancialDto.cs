using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.AdvanceStatement
{
    public class AdvanceStatementFinancialDto
    {
        public int MainSettlementAccountId { get; set; }

        public string MainSettlementAccount { get; set; }

        public long AdvanceStatementId { get; set; }

        public long AdvanceStatementDocumentBaseId { get; set; }

        public int WorkerId { get; set; }

        public string WorkerName { get; set; }

        public decimal AdvanceStatementSum { get; set; }

        public DateTime AdvanceStatementDate { get; set; }

        public List<AdvanceStatementFinancialObjectDto> FinancialObjects { get; set; } = new List<AdvanceStatementFinancialObjectDto>();
    }
}