using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.AccountingV2.Dto.ReconciliationStatements
{
    public class ReconciliationStatementQueryDto
    {
        public DateTime EndDate { get; set; }
        public bool FillForKontragent { get; set; }
        public decimal InitialBalance { get; set; }
        public List<int> KontragentIds { get; set; }
        public DateTime StartDate { get; set; }
        public bool UseStampAndSign { get; set; }
        public DocumentFormat DocFormat { get; set; }
    }
}
