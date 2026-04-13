using System.Collections.Generic;
using Moedelo.BankIntegrations.Dto;

namespace Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Accounts
{
    public class AccountsResponseDto : BaseResponseDto
    {
        public List<AccountDto> Accounts { get; set; }
    }
}
