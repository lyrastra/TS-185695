using System;
using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.KontragentDocuments.ReconciliationStatements
{
    public class ReconciliationStatementRequestDto
    {
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public int AccountCode { get; set; }
        
        public bool FillForKontragent { get; set; }
        
        public List<int> KontragentIds { get; set; }
        
        public List<long> ContractIds { get; set; }
    }
}