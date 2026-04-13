using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesBills.Private.Models;

public class FetchBillModelDto
{
    /// <summary>
    /// Идентификатор счета
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public PaidStatus PaidStatus { get; set; }

    /// <summary>
    /// Дата счета
    /// </summary>
    public DateTime DocumentDate { get; set; }

    /// <summary>
    /// Идентификатор контрагента
    /// </summary>
    public int KontragentId { get; set; }

    /// <summary>
    /// Сумма счета
    /// </summary>
    public decimal Sum { get; set; }
}