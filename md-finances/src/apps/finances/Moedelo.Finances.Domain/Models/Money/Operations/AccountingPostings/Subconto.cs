using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Finances.Domain.Models.Money.Operations.AccountingPostings
{
    public class Subconto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SubcontoType Type { get; set; }
    }
}