using System.Collections.Generic;

namespace Moedelo.SuiteCrm.Dto.LoadLead
{
    public class LoadLeadByButtonResponseDto
    {
        public int FirmId { get; set; }
        public bool Success { get; set; }
        public List<string> ErrorList { get; set; }
    }
}