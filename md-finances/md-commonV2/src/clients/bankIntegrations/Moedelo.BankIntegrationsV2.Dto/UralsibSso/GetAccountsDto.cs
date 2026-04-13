using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.UralsibSso
{
    public class GetAccountsDto
    {
        public TokenDto Token { get; set; }

        public List<AccountInfoDto> AccountsInfo { get; set; }
    }
}
