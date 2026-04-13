using System.Collections.Generic;

namespace Moedelo.Money.Registry.Domain.Models
{
    public class OperationsResult
    {
        public int TotalCount { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public IReadOnlyCollection<MoneyOperation> Operations { get; set; }
    }
}
