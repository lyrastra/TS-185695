using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer;

/// <summary>
/// БИЗ - УС: данные для переноса биллинга
/// </summary>
public class ContextDto
{
    /// <summary>
    /// Платежи, доступные для переноса (идентификаторы)
    /// </summary>
    public List<Payment> Payments { get; set; }

    /// <summary>
    /// Идентификатор текущего платежа, предоставляющего доступ в ЛК
    /// </summary>
    public int CurrentPaymentId { get; set; }

    /// <summary>
    /// Краткая информация о записи в PaymentHistory
    /// </summary>
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
    }
}