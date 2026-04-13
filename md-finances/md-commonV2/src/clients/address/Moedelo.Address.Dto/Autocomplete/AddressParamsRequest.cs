using System;

namespace Moedelo.Address.Dto.Autocomplete
{
    public class AddressParamsRequest
    {
        public bool IsOoo { get; set; }

        public Guid Guid { get; set; }

        public string House { get; set; }

        public string Building { get; set; }

        public string BuildingName { get; set; }
    }
}
