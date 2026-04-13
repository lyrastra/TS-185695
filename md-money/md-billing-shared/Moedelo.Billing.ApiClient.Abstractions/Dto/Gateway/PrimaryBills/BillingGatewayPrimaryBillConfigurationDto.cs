namespace Moedelo.Billing.Abstractions.Dto.Gateway.PrimaryBills;

/// <summary>
/// Информация о ПУ первичного счёта
/// </summary>
public class BillingGatewayPrimaryBillConfigurationDto
{
    /// <summary>
    /// Код продукта
    /// </summary>
    public string ProductCode { get; set; }

    /// <summary>
    /// Код ПУ
    /// </summary>
    public string ProductConfigurationCode { get; set; }

    /// <summary>
    /// Признак разовой ПУ
    /// </summary>
    public bool IsOneTime { get; set; }
}
