using System.Collections.Generic;

namespace Moedelo.BankIntegrations.Dto.UralsibbankSso
{
    public class UralsibClientInfoDto : BaseResponseDto
    {
        public string OrganizationId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Phone { get; set; }
        public string Sno { get; set; }
        public TokenDto Token { get; set; }
        public List<SettlementAccountDto> Accounts { get; set; }
    }
}
