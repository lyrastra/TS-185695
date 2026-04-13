using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class OutsourceProcessingMoneyTableResponse
    {
        public int TotalCount { get; set; }
        public List<OutsourceProcessingMoneyTableOperation> Operations { get; set; } = new List<OutsourceProcessingMoneyTableOperation>();
    }
}