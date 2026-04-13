using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;

/// <summary> Список ошибок интеграции </summary>
public class IntegrationErrorListDto
{
    /// <summary> Список ошибок </summary>
    public List<IntegrationErrorDto> Items { get; set; }

    /// <summary> Общее количество </summary>
    public int TotalCount { get; set; }
}