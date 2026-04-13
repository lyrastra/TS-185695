using System.Collections.Generic;

namespace Moedelo.Address.Dto.Autocomplete
{
    public class AddressIdentityTypesDto
    {
        public List<string> HouseTypes { get; set; }
        public List<string> BuildingTypes { get; set; }
        public List<string> FlatTypes { get; set; }
    }
}
