using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Purchases.Events;

namespace Moedelo.Docs.Common.PurchasesWaybill;

public interface IPurchasesWaybillStatePreparer
{
    /// <summary>
    /// Метод дочитывает по API часть данных, не уместившуюся в kafka-событии 
    /// </summary>
    Task<PurchaseWaybillNewState> RestoreTruncatedDataAsync(PurchaseWaybillNewState state);
}