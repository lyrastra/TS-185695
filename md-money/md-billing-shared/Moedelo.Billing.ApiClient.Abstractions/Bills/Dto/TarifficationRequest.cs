#nullable enable
using System.Collections.Generic;
using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.Bills.Dto;

public class TarifficationRequest
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Идентификатор запроса на выставление счёта
    /// </summary>
    public int RequestId { get; set; }

    /// <summary>
    /// Коллекция значений модификаторов.
    /// Для переопределения значений в выставляемом счёте
    /// </summary>
    public IReadOnlyCollection<OverridenModifierRequest>? OverridenModifiers { get; set; }
    
    /// <summary>
    /// Признак успешности завершения расчёта параметров тарификации
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Использовать фактическое значение
    /// </summary>
    public ExtraDataModeEnum ExtraDataMode { get; set; }
}