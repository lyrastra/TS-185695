using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencySale)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class OutgoingCurrencySaleDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IOutgoingCurrencySaleReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IOutgoingCurrencySaleUpdater updater;

        public OutgoingCurrencySaleDuplicateMerger(
            IOutgoingCurrencySaleReader reader,
            ITaxPostingReader taxPostingReader,
            IOutgoingCurrencySaleUpdater updater)
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
            var saveRequest = OutgoingCurrencySaleMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
