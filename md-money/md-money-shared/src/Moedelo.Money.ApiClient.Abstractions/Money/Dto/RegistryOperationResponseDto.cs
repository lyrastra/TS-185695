using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class RegistryResponseDto
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int TotalCount { get; set; }
        public IReadOnlyCollection<RegistryOperationDto> Operations { get; set; }
    }
}
