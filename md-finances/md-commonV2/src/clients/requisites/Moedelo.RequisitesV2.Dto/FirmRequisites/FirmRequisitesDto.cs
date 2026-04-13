using System;

namespace Moedelo.RequisitesV2.Dto.FirmRequisites
{
    public class FirmRequisitesDto
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public DateTime? FnsWarrantDate { get; set; }
        public DateTime? FnsWarrantEndDate { get; set; }
        public string FsgsCode { get; set; }
        public string PfrAgreementNumber { get; set; }
        public DateTime? PfrAgreementDate { get; set; }
        public bool HasStamp { get; set; }
    }
}
