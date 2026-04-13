using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public interface IRefundFromAccountablePersonCommandWriter
    {
        Task WriteImportAsync(
            ImportRefundFromAccountablePerson commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateRefundFromAccountablePerson commandData);

        Task WriteImportWithMissingEmployeeAsync(
            ImportWithMissingEmployeeRefundFromAccountablePerson commandData);
    }
}