using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyTransferToAccount)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class CurrencyTransferToAccountDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ICurrencyTransferToAccountReader reader;
        private readonly ICurrencyTransferToAccountUpdater updater;

        public CurrencyTransferToAccountDuplicateImporter(
            ICurrencyTransferToAccountReader reader,
            ICurrencyTransferToAccountUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = CurrencyTransferToAccountMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.ProvideInAccounting = true;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
