using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.RequisitesV2.Dto.IncorrectEdsFirm
{
    public class IncorrectEdsFirmDto
    {
        public int FirmId { get; set; }
        
        public SignatureStatus EdsStatus { get; set; }
    }
}