using System.Collections.Generic;

namespace Moedelo.Address.ApiClient.Abstractions.legacy.Dto
{
    public class GetFirmAddressStringListDto
    {
        public List<int> FirmIds { get; set; }
        public bool WithAdditionalInfo { get; set; }
    }
}