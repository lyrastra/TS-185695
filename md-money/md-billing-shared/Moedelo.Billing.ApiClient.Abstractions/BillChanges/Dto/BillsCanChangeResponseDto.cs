namespace Moedelo.Billing.Abstractions.BillChanges.Dto;

public class BillsCanChangeResponseDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Флаг возможности изменения счёта
    /// </summary>
    public bool CanChangeBill { get; set; }

    /// <summary>
    /// Информационное сообщение
    /// </summary>
    public string Message { get; set; }
}
