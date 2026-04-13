using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Alfa;

public class IntegrationDataRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }
    
    /// <summary>
    /// Актуальные счета из банка на текущий день
    /// </summary>
    public List<AccountInfoDto> AccountInfoDtos { get; set; }
    
    /// <summary>
    /// Дата последнего обновления счетов в integrationData
    /// </summary>
    public DateTime UpdateAccountsDate { get; set; }
    
    /// <summary>
    /// Идентификатор партнёрского сервиса
    /// </summary>
    public string ClientId { get; set; }
}


public class AccountInfoDto
{
    public string AccountNumber { get; set; }
    
    /// <summary>
    /// Дата открытия счета из банка
    /// </summary>
    public DateTime? OpenDate { get; set; }

    /// <summary>
    /// Счет без ограничений от банка
    /// </summary>
    public bool IsActive { get; set; }
}