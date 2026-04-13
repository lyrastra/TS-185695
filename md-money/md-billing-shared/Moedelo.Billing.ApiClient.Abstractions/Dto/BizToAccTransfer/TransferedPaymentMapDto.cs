namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer;

public class TransferedPaymentMapDto
{
    /// <summary>
    /// Исходный платеж
    /// </summary>
    public int FromPaymentId { get; set; }

    /// <summary>
    /// Новый платеж, созданный по исходному
    /// </summary>
    public int ToPaymentId { get; set; }

    /// <summary>
    /// Идентификатор основного счёта НБ
    /// </summary>
    public int InvoicedPrimaryBillId { get; set; }
}