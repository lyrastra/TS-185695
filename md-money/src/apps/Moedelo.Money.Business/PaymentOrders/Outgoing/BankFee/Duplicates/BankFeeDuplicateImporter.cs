using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BankFee.Duplicates
{
    [OperationType(OperationType.MemorialWarrantBankFee)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class BankFeeDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IBankFeeReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IBankFeeUpdater updater;

        public BankFeeDuplicateImporter(
            IBankFeeReader reader,
            ITaxPostingReader taxPostingReader,
            IBankFeeUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = BankFeeMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
