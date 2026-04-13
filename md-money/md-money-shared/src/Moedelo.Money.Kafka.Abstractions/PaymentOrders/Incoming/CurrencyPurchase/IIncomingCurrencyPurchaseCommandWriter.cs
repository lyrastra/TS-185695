using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase
{
    public interface IIncomingCurrencyPurchaseCommandWriter
    {
        Task WriteImportAsync(ImportIncomingCurrencyPurchase commandData);
        
        Task WriteImportDuplicateAsync(ImportDuplicateIncomingCurrencyPurchase commandData);
    }
}