using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.StateContracts
{
    public class StateContractsInfoDto
    {
        public decimal? Amount { get; set; }

        public int Count { get; set; }

        public List<StateContractDto> Contracts { get; set; } 
    }
}
