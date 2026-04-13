using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.RelatedFirms
{
    public class RelatedFirmsInfoDto
    {
        public RelatedFirmDto MainFirm { get; set; }
        public List<RelatedFirmDto> AttachedFirms { get; set; }
        
        public class RelatedFirmDto
        {
            public int FirmId { get; set; }
            public string UserLogin { get; set; }
        }
    }
}