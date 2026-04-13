using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Business.AccountingPostings;
using Moedelo.Money.Providing.Business.TaxPostings;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerUnprovider))]
    internal class PaymentFromCustomerUnprovider : IPaymentFromCustomerUnprovider
    {
        private readonly TaxPostingsRemover taxPostingsRemover;
        private readonly AccountingPostingsRemover accPostingsRemover;
        private readonly PaymentOrderProvidingStateSetter providingStateSetter;

        public PaymentFromCustomerUnprovider(
            TaxPostingsRemover taxPostingsRemover,
            AccountingPostingsRemover accPostingsRemover, 
            PaymentOrderProvidingStateSetter providingStateSetter)
        {
            this.taxPostingsRemover = taxPostingsRemover;
            this.accPostingsRemover = accPostingsRemover;
            this.providingStateSetter = providingStateSetter;
        }

        public Task UnprovideAsync(long documentBaseId)
        {
            // связи и бух. справки удаляются по эвенту в md-linkedDocuments
            var removeTaxPostingsTask = taxPostingsRemover.DeleteAndUnsetTaxStatusAsync(documentBaseId);
            var removeAccPostingsTask = accPostingsRemover.DeleteAsync(documentBaseId);
            // к моменту удаления записей о проведении быть не должно... подчищаем, если они все же есть
            var unsetStateByBaseIdTask = providingStateSetter.UnsetByBaseIdAsync(documentBaseId);
            return Task.WhenAll(removeTaxPostingsTask, removeAccPostingsTask, unsetStateByBaseIdTask);
        }
    }
}
