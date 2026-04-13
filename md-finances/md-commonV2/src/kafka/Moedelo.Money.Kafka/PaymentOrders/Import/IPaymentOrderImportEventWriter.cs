using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Import.Events;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Import
{
    public interface IPaymentOrderImportEventWriter : IDI
    {
        Task WriteImportFaiedAsync(string key, string token, ImportFailed eventData);
    }
}