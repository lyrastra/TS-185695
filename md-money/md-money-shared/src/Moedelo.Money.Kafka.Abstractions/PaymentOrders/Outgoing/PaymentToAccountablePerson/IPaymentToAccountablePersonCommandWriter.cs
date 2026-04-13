using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonCommandWriter
    {
        Task WriteImportAsync(
            ImportPaymentToAccountablePerson commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicatePaymentToAccountablePerson commandData);

        Task WriteImportWithMissingEmployeeAsync(
            ImportWithMissingEmployeePaymentToAccountablePerson commandData);

        Task WriteApplyIgnoreNumberAsync(ApplyIgnoreNumberPaymentToAccountablePerson commandData);
    }
}