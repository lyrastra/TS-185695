using System.Collections.Generic;

namespace Moedelo.Money.Api.Models.Registry
{
    public class RegistryResponse
    {
        public int totalCount { get; set; }

        public int offset { get; set; }

        public int limit { get; set; }

        public IReadOnlyCollection<OperationResponseDto> data { get; set; }
    }
}
