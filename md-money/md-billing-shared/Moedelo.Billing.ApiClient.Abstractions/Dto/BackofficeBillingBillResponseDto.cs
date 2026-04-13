namespace Moedelo.Billing.Abstractions.Dto;

public class BackofficeBillingBillResponseDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор основного счета
    /// </summary>
    public int PrimaryBillId { get; set; }

    /// <summary>
    /// Номер
    /// </summary>
    public string Number { get; set; }
}