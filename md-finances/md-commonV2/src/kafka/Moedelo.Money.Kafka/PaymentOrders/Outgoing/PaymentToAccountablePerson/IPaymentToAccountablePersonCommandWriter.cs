using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportPaymentToAccountablePerson commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicatePaymentToAccountablePerson commandData);
        Task WriteImportWithMissingEmployeeAsync(string key, string token, ImportWithMissingEmployeePaymentToAccountablePerson commandData);
    }
}