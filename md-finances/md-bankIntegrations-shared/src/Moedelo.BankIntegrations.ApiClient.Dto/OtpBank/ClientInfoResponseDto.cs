using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.OtpBank;

public class ClientInfoResponseDto
{
    public string OrganizationName { get; set; }
    public string OrganizationInn { get; set; }
    public List<AccountResponseDto> Accounts { get; set; }
}

public class AccountResponseDto
{
    public string Number { get; set; }
    public string DepartmentBic { get; set; }
    public DateTime OpenDate { get; set; }
}

