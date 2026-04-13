using System.Collections.Generic;
using Moedelo.BankIntegrations.ApiClient.Dto.Accounts;
using Moedelo.BankIntegrations.Dto;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Rshb;

public class ClientInfoResponseDto : BaseResponseDto
{
    public string RequestId { get; set; }
    public string ClientId { get; set; }
    public ClientDetailInfoResponseDto ClientInformation { get; set; }
}

public class ClientDetailInfoResponseDto
{
    public string Id { get; set; }
    public string Inn { get; set; }
    public string Kpp { get; set; }
    public List<AccountDto> AccountList { get; set; }
}
