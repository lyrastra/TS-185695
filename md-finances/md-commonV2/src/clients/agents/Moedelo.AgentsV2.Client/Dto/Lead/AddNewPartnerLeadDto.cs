using Moedelo.Common.Enums.Enums.Account;

namespace Moedelo.AgentsV2.Client.Dto.Lead
{
    public class AddNewPartnerLeadDto
    {
        public int UserId { get; set; }

        public int? PartnerId { get; set; }

        public long? ReferralLink { get; set; }

        public ProductPlatform ProductPlatform { get; set; }
    }
}