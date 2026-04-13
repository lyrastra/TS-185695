using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerCommandWriter
    {
        Task WriteImportAsync(
            ImportRefundToCustomer commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateRefundToCustomer commandData);

        Task WriteImportWithMissingContractorAsync(
            ImportWithMissingContractorRefundToCustomer commandData);
    }
}