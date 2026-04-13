using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.RefundToCustomer.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportRefundToCustomer commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateRefundToCustomer commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorRefundToCustomer commandData);

    }
}