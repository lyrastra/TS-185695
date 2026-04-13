using System;

namespace Moedelo.Address.ApiClient.Abstractions.legacy.Dto
{
    public class HouseDto
    {
        public string HouseNum { get; set; }
        public string BuildNum { get; set; }
        public Guid HouseGuid { get; set; }
        public Guid AoGuid { get; set; }
    }
}