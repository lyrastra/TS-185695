using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Alfa;

public class ValidAccountsRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }
    
    /// <summary>
    /// Список счетов на проверку из банка
    /// </summary>
    public List<AccountResponseDto> Accounts { get; set; }
}