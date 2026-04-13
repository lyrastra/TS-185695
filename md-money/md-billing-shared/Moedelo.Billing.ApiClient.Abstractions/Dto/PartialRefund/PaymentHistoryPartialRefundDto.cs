using System;

namespace Moedelo.Billing.Abstractions.Dto.PartialRefund;

public class PaymentHistoryPartialRefundDto
{
    public int FirmId { get; set; }
    public int PaymentHistoryId { get; set; }
    /// <summary>
    /// Данные о позициях платежа частичного возврата
    /// </summary>
    public PartialRefundPositionDataDto[] PositionsData { get; set; }
    /// <summary>
    /// Признак активного частичного возврата
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// Дата изменения записи
    /// </summary>
    public DateTime ModifyDate { get; set; }
}