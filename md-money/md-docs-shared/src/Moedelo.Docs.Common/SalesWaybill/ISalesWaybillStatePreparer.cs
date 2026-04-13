using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;

namespace Moedelo.Docs.Common.SalesWaybill;

public interface ISalesWaybillStatePreparer
{
    /// <summary>
    /// Метод дочитывает по API часть данных, не уместившуюся в kafka-событии 
    /// </summary>
    Task<SaleWaybillNewState> RestoreTruncatedDataAsync(SaleWaybillNewState state);
}