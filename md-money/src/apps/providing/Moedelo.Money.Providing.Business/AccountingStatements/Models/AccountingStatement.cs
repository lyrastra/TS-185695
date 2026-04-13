using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Providing.Business.AccountingStatements.Models
{
    class AccountingStatement
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}
