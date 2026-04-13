using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments.Requests
{
    public class GetPaymentDocumentLastNumbersRequestDto
    {
        public GetPaymentDocumentLastNumbersRequestDto()
        {
            SalaryProjectSettlementAccountIds = new List<int>();            
        }
        
        public int Year { get; set; }

        public int? SettlementAccountId { get; set; }
        
        public List<int> SalaryProjectSettlementAccountIds { get; set; }
    }
}