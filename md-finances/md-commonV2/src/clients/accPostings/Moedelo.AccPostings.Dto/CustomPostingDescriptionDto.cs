using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using System;
using System.Collections.Generic;

namespace Moedelo.AccPostings.Dto
{
    public class CustomPostingDescriptionDto
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public SyntheticAccountCode Debit { get; set; }
        public SyntheticAccountCode Credit { get; set; }
        public string Description { get; set; }
        public List<long> DebitSubcontoIds { get; set; }
        public List<long> CreditSubcontoIds { get; set; }
    }
}
