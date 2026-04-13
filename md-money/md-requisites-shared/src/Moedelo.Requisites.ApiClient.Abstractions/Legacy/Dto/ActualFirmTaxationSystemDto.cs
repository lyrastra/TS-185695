using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class ActualFirmTaxationSystemDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public bool IsUsn { get; set; }

        public bool IsEnvd { get; set; }

        public UsnType UsnType { get; set; }

        public bool IsOsno { get; set; }

        public bool IsEshn { get; set; }

        public bool IsPatent { get; set; }
    }
}
