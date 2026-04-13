using Moedelo.Common.Enums.Enums.Account;
using Moedelo.Common.Enums.Enums.Agents;

namespace Moedelo.AgentsV2.Client.Dto.Partner
{
    public class PartnerRewardSettingDto
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public LeadEventType EventType { get; set; }
        public bool RewardType { get; set; }
        public decimal RewardValue { get; set; }
        public ProductPlatform ProductPlatform { get; set; }
        public PartnerRewardSettingConditionsDto RewardConditions { get; set; }
        public int CreateUserId { get; set; }
        public int ModifyUserId { get; set; }
    }
}