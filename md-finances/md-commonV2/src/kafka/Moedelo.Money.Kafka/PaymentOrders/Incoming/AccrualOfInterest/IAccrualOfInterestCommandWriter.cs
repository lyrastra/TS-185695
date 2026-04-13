using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.AccrualOfInterest.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.AccrualOfInterest
{
    public interface IAccrualOfInterestCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportAccrualOfInterest commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateAccrualOfInterest commandData);
    }
}