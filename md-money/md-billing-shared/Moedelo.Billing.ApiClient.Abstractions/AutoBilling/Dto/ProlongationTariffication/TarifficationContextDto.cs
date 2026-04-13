using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto.ProlongationTariffication;

/// <summary>
/// Контекст получения параметров тарификации
/// </summary>
public class TarifficationContextDto
{
    /// <summary>
    /// Идентификатор фирмы 
    /// </summary>
    public int FirmId { get; set; }
    /// <summary>
    /// Идентификатор запроса выставления счета 
    /// </summary>
    public int? RequestId { get; set; }
    /// <summary>
    /// Код тарифицируемой ПУ
    /// </summary>
    public string ProductConfigurationCode { get; set; }
    /// <summary>
    /// Фактическое количество сотрудников фирмы 
    /// </summary>
    public int? ActualWorkersCount { get; set; }
    /// <summary>
    /// Текущий лимит среднемесячного оборота
    /// </summary>
    public int? AverageMoneyTurnoverLimit { get; set; }
    /// <summary>
    /// Фактический среднемесячный оборот
    /// </summary>
    public int? ActualAverageTurnover { get; set; }
    /// <summary>
    /// Текущий лимит количества сотрудников
    /// </summary>
    public int? WorkersCountLimit { get; set; }
    /// <summary>
    /// Дополнительные опции
    /// </summary>
    public IReadOnlyCollection<string> AdditionalOptions { get; set; }
}