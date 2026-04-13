using System;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.PaidConfigurations.Dto;

public class PaidConfigurationsRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Начало периода, в котором должны действовать оплаченные ПУ. Опционально
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Окончание периода, в котором должны действовать оплаченные ПУ. Опционально
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Список партнёров, являющихся продавцами ПУ. Опционально (может быть null или пустым массивом)
    /// </summary>
    public RegionalPartnerType[] RegionalPartnerTypes { get; set; }

    /// <summary>
    /// Флаг исключения разовых услуг
    /// </summary>
    public bool ExcludeOneTime { get; set; } = false;

    /// <summary>
    /// Флаг исключения допродаж
    /// </summary>
    public bool ExcludeCrossSelling { get; set; } = false;
}