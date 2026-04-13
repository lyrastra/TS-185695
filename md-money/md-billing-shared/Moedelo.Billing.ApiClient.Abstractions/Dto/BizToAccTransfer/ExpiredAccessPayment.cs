using System;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer;

/// <summary>
/// Результат операции (бэкап изменившихся полей): Устанавливает существующий платеж "ПРОСРОЧЕННЫМ ТЕХНИЧЕСКИМ"
/// </summary>
public class ExpiredAccessPayment
{
    public class RequestDto
    {
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        public int PaymentId { get; set; }

        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }
    }

    public class ResultDto
    {
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        public int PaymentId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsDownload { get; set; }
    }
}