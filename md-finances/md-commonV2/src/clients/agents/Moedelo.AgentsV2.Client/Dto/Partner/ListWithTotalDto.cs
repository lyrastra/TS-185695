using System.Collections.Generic;

namespace Moedelo.AgentsV2.Client.Dto.Partner
{
    public class ListWithTotalDto<T>
    {
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}