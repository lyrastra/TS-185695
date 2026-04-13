using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RequisitesV2.Dto.FirmTaxationSystem
{
    public class ActualFirmTaxationSystemDto
    {
        public int Id { get; set; }
        
        public int FirmId { get; set; }

        public bool IsUsn { get; set; }

        public bool IsEnvd { get; set; }

        public UsnTypes UsnType { get; set; }

        public bool IsOsno { get; set; }

        public bool IsEshn { get; set; }

        public bool IsPatent { get; set; }
    }
}