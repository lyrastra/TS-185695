namespace Moedelo.Address.Dto.Autocomplete
{
    public class AddressPreviewRequest
    {
        public string Code { get; set; }
        public string HouseName { get; set; }
        public string House { get; set; }
        public string BuildingName { get; set; }
        public string Building { get; set; }
        public string FlatName { get; set; }
        public string Flat { get; set; }
        public string PostIndex { get; set; }
        public string AdditionalInfo { get; set; }
        public bool WithAdditionalInfo { get; set; }
    }
}
