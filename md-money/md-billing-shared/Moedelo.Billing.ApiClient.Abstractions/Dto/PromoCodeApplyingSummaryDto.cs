namespace Moedelo.Billing.Abstractions.Dto;

public class PromoCodeApplyingSummaryDto
{
    /// <summary>
    /// промокод, который предлагалось применить
    /// </summary>
    public string PromoCode { get; set; }

    /// <summary>
    /// промокод был найден
    /// </summary>
    public bool IsFound { get; set; }

    /// <summary>
    /// промокод был применён
    /// </summary>
    public bool IsApplied { get; set; }

    /// <summary>
    /// ошибка (если присутствует)
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// описание действия промокода
    /// </summary>
    public string Description { get; set; }
}