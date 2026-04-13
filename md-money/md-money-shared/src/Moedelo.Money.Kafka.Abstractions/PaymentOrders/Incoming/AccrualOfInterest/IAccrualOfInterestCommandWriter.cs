using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest
{
    public interface IAccrualOfInterestCommandWriter
    {
        Task WriteImportAsync(ImportAccrualOfInterest commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateAccrualOfInterest commandData);
    }
}