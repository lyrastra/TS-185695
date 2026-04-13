using System;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer;

/// <summary>
/// Результат операции (бэкап изменившихся полей): Устанавливает существующий платеж "ТЕХНИЧЕСКИМ" с указанной датой окончания
/// </summary>
public class Payment
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

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime ExpirationDate { get; set; }
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