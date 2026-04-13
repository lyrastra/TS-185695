using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUkd.Model;

public class SalesUkdSelfCostDto
{
    /// <summary>
    /// Идентификатор УКД
    /// </summary>
    public long DocumentBaseId { get; set; }

    /// <summary>
    /// Номер УКД
    /// </summary>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Дата УКД
    /// </summary>
    public DateTime DocumentDate { get; set; }

    /// <summary>
    /// Документ на основании которого создан УКД
    /// </summary>
    /// <remarks>Для определения "Учитывается в СНО" по исходному документу</remarks>
    public long SourceDocumentBaseId { get; set; }

    /// <summary>
    /// Список позиций УКД
    /// </summary>
    public IReadOnlyList<SalesUkdItemSelfCostDto> Items { get; set; }
}