using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.Finances.Domain.Models.Money.Operations.AccountingPostings
{
    public class AccountingPosting
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public SyntheticAccountCode DebitCode { get; set; }

        public SyntheticAccountCode CreditCode { get; set; }

        public long DebitTypeId { get; set; }

        public long CreditTypeId { get; set; }

        public string DebitNumber { get; set; }

        public string CreditNumber { get; set; }

        public string Description { get; set; }

        public List<Subconto> DebitSubcontos { get; set; }

        public List<Subconto> CreditSubcontos { get; set; }

        public SyntheticAccountBalanceType CreditBalanceType { get; set; }

        public SyntheticAccountBalanceType DebitBalanceType { get; set; }
    }
}