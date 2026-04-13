using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee.Duplicates
{
    [OperationType(OperationType.CurrencyBankFee)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class CurrencyBankFeeDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ICurrencyBankFeeReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly ICurrencyBankFeeUpdater updater;

        public CurrencyBankFeeDuplicateMerger(
            ICurrencyBankFeeReader reader,
            ITaxPostingReader taxPostingReader,
            ICurrencyBankFeeUpdater updater)
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
            var saveRequest = CurrencyBankFeeMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
