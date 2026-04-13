using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentResaveService))]
    class BudgetaryPaymentResaveService : IBudgetaryPaymentResaveService
    {
        private readonly IBudgetaryPaymentReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IBudgetaryPaymentUpdater updater;

        public BudgetaryPaymentResaveService(
            IBudgetaryPaymentReader reader, 
            ITaxPostingReader taxPostingReader, 
            IBudgetaryPaymentUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ResaveAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = BudgetaryPaymentMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
