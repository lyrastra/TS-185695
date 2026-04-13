using System;

namespace Moedelo.Billing.Abstractions.Dto.PartialRefund;

public class PartialRefundPositionDataDto
{
    /// <summary>
    /// Номер позиции платежа
    /// </summary>
    public int ItemNumber { get; set; }

    /// <summary>
    /// Первоначальная дата начла срока действия из позиции платежа
    /// </summary>
    public DateTime? ItemStartDate { get; set; }
        
    /// <summary>
    /// Первоначальная дата завершения срока действия из позиции платежа
    /// </summary>
    public DateTime? ItemEndDate { get; set; }
}