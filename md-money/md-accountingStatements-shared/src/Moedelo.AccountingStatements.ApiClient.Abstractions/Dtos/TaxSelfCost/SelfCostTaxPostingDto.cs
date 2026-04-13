using System;
using Moedelo.AccountingStatements.Enums;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos
{
    public class SelfCostTaxPostingDto
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public string DocumentNumber { get; set; }

        public TaxPostingsDirection Direction { get; set; }
    }
}