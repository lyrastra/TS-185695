using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingRefundToCustomer)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class RefundToCustomerDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IRefundToCustomerReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IRefundToCustomerUpdater updater;

        public RefundToCustomerDuplicateMerger(
            IRefundToCustomerReader reader,
            ITaxPostingReader taxPostingReader,
            IRefundToCustomerUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task MergeAsync(PaymentOrderDuplicateMergeRequest request)
        {
            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false);
            if (response.Date == request.Date)
            {
                return;
            }
            var saveRequest = RefundToCustomerMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
