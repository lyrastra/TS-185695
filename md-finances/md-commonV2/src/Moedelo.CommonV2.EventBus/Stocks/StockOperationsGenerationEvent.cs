using Moedelo.Common.Enums.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Stocks;

/// <summary>
/// Событие "Запустить генерацию данных для Движения"
/// </summary>
public class StockOperationsGenerationEvent
{
    public int FirmId { get; set; }
    public int UserId { get; set; }
    public string Query { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? StartSum { get; set; }
    public decimal? EndSum { get; set; }
    public IReadOnlyCollection<int> TypeList { get; set; }
    public IReadOnlyCollection<long> StockIdList { get; set; }
    public IReadOnlyCollection<int> KontragentIdList { get; set; }
    public IReadOnlyCollection<int> WorkerIdList { get; set; }
    public IReadOnlyCollection<int> PaymentStatusList { get; set; }
    public IReadOnlyCollection<long> ProductIdList { get; set; }
    public uint Offset { get; set; }
    public uint Limit { get; set; }
    public int SortBy { get; set; }
    public SortOrder SortOrder { get; set; }
    public bool ClearCache { get; set; }
}