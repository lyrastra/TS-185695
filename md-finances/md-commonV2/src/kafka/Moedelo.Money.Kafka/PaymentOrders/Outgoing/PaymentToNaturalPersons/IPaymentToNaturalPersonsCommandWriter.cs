using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportPaymentToNaturalPersons commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicatePaymentToNaturalPersons commandData);
        Task WriteImportWithMissingEmployeeAsync(string key, string token, ImportWithMissingEmployeePaymentToNaturalPersons commandData);
    }
}