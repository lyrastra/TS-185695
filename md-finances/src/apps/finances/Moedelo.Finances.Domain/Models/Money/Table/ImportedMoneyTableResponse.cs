using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class ImportedMoneyTableResponse
    {
        public int TotalCount { get; set; }
        public List<ImportedMoneyTableOperation> Operations { get; set; } = new List<ImportedMoneyTableOperation>();
    }
}