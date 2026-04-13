using Moedelo.Common.Enums.Enums.Accounting;
using System;

namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    public class AccountingStatementSimpleDto
    {
        public DateTime Date { get; set; }

        public long Id { get; set; }

        public long? DocumentBaseId { get; set; }

        public string Number { get; set; }

        public AccountingStatementType Type { get; set; }

        public PrimaryDocumentsMoneyDirection Direction { get; set; }

        public string Description { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public decimal BuyoutSum { get; set; }
    }
}
