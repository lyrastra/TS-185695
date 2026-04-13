using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToSupplier)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class PaymentToSupplierDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IPaymentToSupplierReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IPaymentToSupplierUpdater updater;

        public PaymentToSupplierDuplicateImporter(
            IPaymentToSupplierReader reader,
            ITaxPostingReader taxPostingReader,
            IPaymentToSupplierUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            if (response.Contract.Status != RemoteServiceStatus.Ok ||
                response.Documents.Status != RemoteServiceStatus.Ok)
            {
                throw new RemoteServiceException();
            }
            var saveRequest = PaymentToSupplierMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
