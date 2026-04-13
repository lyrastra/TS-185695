using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingRefundToCustomer)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class RefundToCustomerDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IRefundToCustomerReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IRefundToCustomerUpdater updater;

        public RefundToCustomerDuplicateImporter(
            IRefundToCustomerReader reader,
            ITaxPostingReader taxPostingReader,
            IRefundToCustomerUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            if (response.Contract.Status != RemoteServiceStatus.Ok)
            {
                throw new RemoteServiceException();
            }
            var saveRequest = RefundToCustomerMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
