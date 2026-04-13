namespace Moedelo.Billing.Abstractions.LimitExcessData.Dto;

public class LimitValueResponseDto
{
    /// <summary>
    /// Значение запрошенного лимимта
    /// </summary>
    public int FeatureLimitValue { get; set; }

    /// <summary>
    /// Код лимита
    /// </summary>
    public string FeatureLimitCode { get; set; }
}