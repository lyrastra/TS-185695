using System.Collections.Generic;

namespace Moedelo.AccPostings.Dto
{
    public class AccountBalanceDivisionDto
    {
        public long DivisionSubcontoId { get; set; }
        public List<SyntheticAccountBalanceDto> AccountBalance { get; set; } 
    }
}