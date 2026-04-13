using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesBills.Private.Models;

public class FetchBillsRequestDto
{
    /// <summary>
    /// Идентификатор последнего полученного счета
    /// </summary>
    public int LastBillId { get; set; }

    /// <summary>
    /// Идентификатор фирмы последнего полученного счета
    /// </summary>
    public int LastFirmId { get; set; }

    /// <summary>
    /// Кол-во счетов (по умолчанию 5_000)
    /// </summary>
    public int Limit { get; set; } = 5_000;

    /// <summary>
    /// Фильтр по дате документа: дата больше или равна переданному значению
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Фильтр по дате документа: дата меньше или равна переданному значению
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Фильтр по статусу оплаты: статус равен хотя бы одному значению из списка
    /// </summary>
    public List<PaidStatus> PaidStatuses { get; set; }

    /// <summary>
    /// Фильтр по дате оплаты: дата больше или равна переданному значению
    /// </summary>
    public DateTime? StartDueDate { get; set; }

    /// <summary>
    /// Фильтр по дате оплаты: дата меньше или равна переданному значению
    /// </summary>
    public DateTime? EndDueDate { get; set; }
}