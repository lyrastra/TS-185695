namespace Moedelo.Billing.Abstractions.BillChanges.Dto;

public class BillChangeTaxationSystemRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Тип СНО
    /// </summary>
    public bool IsOsno { get; set; }

    /// <summary>
    /// Идентификатор пользователя, инициировавшего изменение
    /// </summary>
    public int AuthorUserId { get; set; }
}
