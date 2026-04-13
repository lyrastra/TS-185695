using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RequisitesV2.Dto.FirmTaxationSystem
{
    public class FirmTaxationSystemDto
    {
        public int Id { get; set; }
        public short StartYear { get; set; }
        public short? EndYear { get; set; }
        public bool IsUSN { get; set; }
        public bool IsENVD { get; set; }
        public bool IsOSNO { get; set; }
        public bool IsESHN { get; set; }
        public bool IsUsnPatent { get; set; }
        public UsnTypes USNType { get; set; }
        public double? USNSize { get; set; }
    }
}