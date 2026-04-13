using System;

namespace Moedelo.Authorization.Dto;

/// <summary>
/// Описание установленного для фирмы функционального лимита
/// </summary>
public class FirmFeatureLimitDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Функциональный лимит
    /// </summary>
    public FeatureLimitId FeatureLimitId { get; set; }
        
    /// <summary>
    /// Технический код функционального лимита
    /// заполняется только для лимитов из продуктового биллинга, иначе равен null
    /// </summary>
    public string FeatureLimitCode { get; set; }

    /// <summary>
    /// Значение лимита
    /// </summary>
    public int MaxValue { get; set; }

    /// <summary>
    /// Дата начала действия лимита
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания действия лимита (может быть null - бессрочный лимит)
    /// </summary>
    public DateTime? EndDate { get; set; }
}