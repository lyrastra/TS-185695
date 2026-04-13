using Moedelo.BankIntegrations.Dto;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Tinkoff
{
    public class TinkoffClientInfoDto
    {
        public string Active { get; set; }

        public List<string> Scope { get; set; }

        public TokenDto Token { get; set; }
    }
}
