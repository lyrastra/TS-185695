using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue.Duplicates
{
    [OperationType(OperationType.MemorialWarrantRetailRevenue)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class RetailRevenueDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IRetailRevenueReader reader;
        private readonly IRetailRevenueUpdater updater;
        private readonly ITaxPostingReader taxPostingReader;

        public RetailRevenueDuplicateImporter(
            IRetailRevenueReader reader,
            IRetailRevenueUpdater updater,
            ITaxPostingReader taxPostingReader)
        {
            this.reader = reader;
            this.updater = updater;
            this.taxPostingReader = taxPostingReader;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = RetailRevenueMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
