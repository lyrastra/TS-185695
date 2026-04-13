using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee.Duplicates
{
    [OperationType(OperationType.CurrencyBankFee)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class CurrencyBankFeeDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ICurrencyBankFeeReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly ICurrencyBankFeeUpdater updater;

        public CurrencyBankFeeDuplicateImporter(
            ICurrencyBankFeeReader reader,
            ITaxPostingReader taxPostingReader,
            ICurrencyBankFeeUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = CurrencyBankFeeMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
