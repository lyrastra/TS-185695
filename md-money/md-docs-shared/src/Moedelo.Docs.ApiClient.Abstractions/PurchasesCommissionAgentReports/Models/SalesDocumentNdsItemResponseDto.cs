using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models;

public class SalesDocumentNdsItemResponseDto
{
    /// <summary>
    /// Доступное для возврата кол-во
    /// </summary>
    public decimal AvailableCountToRefund { get; set; }

    /// <summary>
    /// Доступная для возврата сумма
    /// </summary>
    public decimal AvailableSumToRefund { get; set; }

    /// <summary>
    /// Ставка НДС
    /// </summary>
    public NdsType NdsType { get; set; }

    /// <summary>
    /// Минимальная дата отгрузки
    /// </summary>
    public DateTime MinShipmentDate { get; set; }
}