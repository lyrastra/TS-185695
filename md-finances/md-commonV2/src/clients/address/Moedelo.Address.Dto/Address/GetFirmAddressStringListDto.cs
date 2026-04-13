using System.Collections.Generic;

namespace Moedelo.Address.Dto.Address
{
    public class GetFirmAddressStringListDto
    {
        public List<int> FirmIds { get; set; }
        public bool WithAdditionalInfo { get; set; }
    }
}