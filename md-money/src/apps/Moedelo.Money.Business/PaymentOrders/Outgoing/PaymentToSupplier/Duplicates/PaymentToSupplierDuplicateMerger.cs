using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToSupplier)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class PaymentToSupplierDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IPaymentToSupplierReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IPaymentToSupplierUpdater updater;

        public PaymentToSupplierDuplicateMerger(
            IPaymentToSupplierReader reader,
            ITaxPostingReader taxPostingReader,
            IPaymentToSupplierUpdater updater)
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
            var saveRequest = PaymentToSupplierMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
