using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models
{
    public class ListWithCount<T>
    {
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
