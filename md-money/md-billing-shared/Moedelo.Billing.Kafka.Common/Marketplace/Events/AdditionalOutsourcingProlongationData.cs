namespace Moedelo.Billing.Kafka.Common.Marketplace.Events;

/// <summary>
/// Дополнительные данные продления аутсорсинга
/// </summary>
public class AdditionalOutsourcingProlongationData
{
    /// <summary>
    /// Действующий лимит среднемесячного оборота
    /// </summary>
    public int? AverageMoneyTurnoverLimit { get; set; }
    
    /// <summary>
    /// Последний действующий лимит среднемесячного оборота
    /// </summary>
    public int? LastAverageMoneyTurnoverLimit { get; set; }

    /// <summary>
    /// Последнее количество сотрудников 
    /// </summary>
    public int? LastWorkersCount { get; set; }

    /// <summary>
    /// Последнее количество сотрудников с КЭДО
    /// </summary>
    public int? LastKedoWorkersCount { get; set; }

    /// <summary>
    /// Действующий лимит количества сотрудников
    /// </summary>
    public int? WorkersCountLimit { get; set; }

    /// <summary>
    /// Действующий лимит количества сотрудников для корпоративного электронного документооборота
    /// </summary>
    public int? KedoWorkersCountLimit { get; set; }

    /// <summary>
    /// Последний действующий лимит количества сотрудников
    /// </summary>
    public int? LastWorkersCountLimit { get; set; }

    /// <summary>
    /// Фактический среднемесячный оборот
    /// </summary>
    public int? ActualAverageTurnover { get; set; }

    /// <summary>
    /// Фактическое количество сотрудников фирмы
    /// </summary>
    public int? ActualWorkersCount { get; set; }
}