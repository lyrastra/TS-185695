using System.Collections.Generic;

namespace Moedelo.Address.Dto.Address
{
    public class GetAddressStringListDto
    {
        public List<long> AddressIds { get; set; }
        public bool WithAdditionalInfo { get; set; }
    }
}