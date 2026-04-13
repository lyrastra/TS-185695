using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Duplicates
{
    [OperationType(OperationType.BudgetaryPayment)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal class BudgetaryPaymentDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IBudgetaryPaymentReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IBudgetaryPaymentUpdater updater;

        public BudgetaryPaymentDuplicateMerger(
            IBudgetaryPaymentReader reader,
            ITaxPostingReader taxPostingReader,
            IBudgetaryPaymentUpdater updater)
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
            var saveRequest = BudgetaryPaymentMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
