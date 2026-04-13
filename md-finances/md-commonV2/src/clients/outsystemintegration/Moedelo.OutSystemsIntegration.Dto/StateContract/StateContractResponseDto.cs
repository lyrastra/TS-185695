using System.Collections.Generic;

namespace Moedelo.OutSystemsIntegrationV2.Dto.StateContract
{
    public class StateContractResponseDto
    {
        public List<StateContractDto> Contracts { get; set; }

        public int Count { get; set; }

        public decimal? Amount { get; set; }
    }
}
