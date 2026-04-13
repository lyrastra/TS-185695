using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Money.Table
{
    public class MoneyTableResponseClientData<T>
        where T : MoneyOperationClientData
    {
        public int TotalCount { get; set; }
        public IReadOnlyCollection<T> Operations { get; set; } = new List<T>();
    }
}