using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class BillsForContractDetailsDto
    {
        public decimal Sum { get; set; }

        public decimal PaidSum { get; set; }
        
        public List<BillForContractDetailsDto> List { get; set; }
    }
}

