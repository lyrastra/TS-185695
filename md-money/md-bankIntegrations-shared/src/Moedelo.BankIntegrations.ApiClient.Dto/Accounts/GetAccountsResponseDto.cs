using Moedelo.BankIntegrations.Dto;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Accounts
{
    public class GetAccountsResponseDto : BaseResponseDto
    {
        public List<AccountDto> Accounts { get; set; }
    }
}
