using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RequisitesV2.Dto.FirmTaxationSystem
{
    public class TaxationSystemDto
    {
        public int Id { get; set; }
        public short StartYear { get; set; }
        public short? EndYear { get; set; }

        public bool IsUsn { get; set; }
        public UsnTypes UsnType { get; set; }
        public double? UsnSize { get; set; }

        public bool IsEnvd { get; set; }

        public bool IsOsno { get; set; }

        public bool IsPatent { get; set; }
    }
}