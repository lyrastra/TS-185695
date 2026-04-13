using System.Collections.Generic;
using Moedelo.AgentsV2.Dto.Partners;

namespace Moedelo.AgentsV2.Client.Dto.Partner
{
    public class PartnerRewardSettingsDto
    {
        public PartnerInfoDto Partner { get; set; }

        public List<PartnerRewardSettingDto> PartnerRewardSettings { get; set; }
    }
}