using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Purchases;

namespace Moedelo.Docs.Common.PurchasesUpd;

public interface IPurchasesUpdStatePreparer
{
    /// <summary>
    /// Метод дочитывает по API часть данных, не уместившуюся в kafka-событии 
    /// </summary>
    Task<PurchaseUpdNewState> RestoreTruncatedDataAsync(PurchaseUpdNewState state);
}