using System.Collections.Generic;

namespace Moedelo.Money.Domain.Registry
{
    public class RegistryResponse
    {
        public int TotalCount { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public IReadOnlyCollection<RegistryOperation> Operations { get; set; }
    }
}
