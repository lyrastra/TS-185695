using System.Collections.Generic;

namespace Moedelo.SuiteCrm.Dto.LeadInfo
{
    public class UpdatePartnerDto
    {
        public List<int> FirmIds { get; set; }

        public string PartnerName { get; set; }
    }
}