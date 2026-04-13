using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccountingV2.Dto.StockDebitOperation
{
    public class PostingDescriptionDto
    {
        public long Id { get; set; }

        public string Date { get; set; }

        public decimal Sum { get; set; }

        public int Debit { get; set; }

        public int Credit { get; set; }

        public long DebitTypeId { get; set; }

        public long CreditTypeId { get; set; }

        public string DebitNumber { get; set; }

        public string CreditNumber { get; set; }

        public string Description { get; set; }

        public List<SubcontoDto> SubcontoDebit { get; set; }

        public List<SubcontoDto> SubcontoCredit { get; set; }

        public SyntheticAccountBalanceType CreditBalanceType { get; set; }

        public SyntheticAccountBalanceType DebitBalanceType { get; set; }
    }
}
