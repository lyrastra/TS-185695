namespace Moedelo.Billing.Abstractions.BillChanges.Dto;

public class BillsChangeLegalTypeRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Вид ОПФ
    /// </summary>
    public bool IsOoo { get; set; }

    /// <summary>
    /// Идентификатор пользователя, инициировавшего изменение
    /// </summary>
    public int AuthorUserId { get; set; }
}
