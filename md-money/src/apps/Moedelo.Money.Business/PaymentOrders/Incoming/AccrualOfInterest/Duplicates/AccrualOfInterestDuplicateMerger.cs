using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest.Duplicates
{
    [OperationType(OperationType.MemorialWarrantAccrualOfInterest)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class AccrualOfInterestDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IAccrualOfInterestReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IAccrualOfInterestUpdater updater;

        public AccrualOfInterestDuplicateMerger(
            IAccrualOfInterestReader reader,
            ITaxPostingReader taxPostingReader,
            IAccrualOfInterestUpdater updater)
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
            var saveRequest = AccrualOfInterestMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
