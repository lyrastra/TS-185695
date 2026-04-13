using System.Collections.Generic;

namespace Moedelo.RptV2.Dto.ReconciliationStatements
{
    public class ReconciliationStatementReportDto
    {
        public List<int> KontragentIds { get; set; }

        public string Content { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }
    }
}
