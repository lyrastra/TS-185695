using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.AdvanceStatement
{
    public class WorkerBalanceDto
    {
        public List<AdvancePaymentDocumentDto> Documents { get; set; } = new List<AdvancePaymentDocumentDto>();
        
        public bool ResponseStatus { get; set; }
        
        public string ResponseMessage { get; set; }
    }
}