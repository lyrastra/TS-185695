using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales;

namespace Moedelo.Docs.Common.Services.SalesUpds;

public interface ISalesUpdStatePreparer
{
    /// <summary>
    /// Метод дочитывает по API часть данных, не уместившуюся в kafka-событии 
    /// </summary>
    Task<SaleUpdNewState> RestoreTruncatedDataAsync(SaleUpdNewState state);
}