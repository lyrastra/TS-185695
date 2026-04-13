using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue.Duplicates
{
    [OperationType(OperationType.MemorialWarrantRetailRevenue)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class RetailRevenueDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IRetailRevenueReader reader;
        private readonly IRetailRevenueUpdater updater;
        private readonly ITaxPostingReader taxPostingReader;

        public RetailRevenueDuplicateMerger(
            IRetailRevenueReader reader,
            IRetailRevenueUpdater updater,
            ITaxPostingReader taxPostingReader)
        {
            this.reader = reader;
            this.updater = updater;
            this.taxPostingReader = taxPostingReader;
        }

        public async Task MergeAsync(PaymentOrderDuplicateMergeRequest request)
        {
            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false);
            if (response.Date == request.Date)
            {
                return;
            }
            var saveRequest = RetailRevenueMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
