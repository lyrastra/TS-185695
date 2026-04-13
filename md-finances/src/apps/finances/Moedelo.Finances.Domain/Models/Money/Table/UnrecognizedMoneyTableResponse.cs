using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class UnrecognizedMoneyTableResponse
    {
        public int TotalCount { get; set; }
        public IReadOnlyCollection<UnrecognizedMoneyTableOperation> Operations { get; set; } = Array.Empty<UnrecognizedMoneyTableOperation>();
    }
}