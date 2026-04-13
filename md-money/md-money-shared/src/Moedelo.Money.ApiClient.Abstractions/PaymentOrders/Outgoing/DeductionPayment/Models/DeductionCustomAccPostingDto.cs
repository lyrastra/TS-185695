using System.Collections.Generic;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.AccPostings;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.DeductionPayment.Models;

public class DeductionCustomAccPostingDto
{
    /// <summary>
    /// Сумма
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Идентификатор объекта учета (дебет)
    /// Поле "SubcontoId"
    /// </summary>
    public IReadOnlyCollection<SubcontoDto> DebitSubconto { get; set; }

    /// <summary>
    /// Код учета (дебет)
    /// </summary>
    public int DebitCode { get; set; }

    /// <summary>
    /// Идентификаторы объектов учета (кредит)
    /// </summary>
    public long CreditSubconto { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
}