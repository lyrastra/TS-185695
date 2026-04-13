using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class FirmTaxationSystemDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public short StartYear { get; set; }

        public short? EndYear { get; set; }

        public bool IsUsn { get; set; }

        public UsnType UsnType { get; set; }

        public double? UsnSize { get; set; }

        public bool IsEnvd { get; set; }

        public bool IsOsno { get; set; }
    }
}
