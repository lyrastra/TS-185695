using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyTransferFromAccount)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class CurrencyTransferFromAccountDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ICurrencyTransferFromAccountReader reader;
        private readonly ICurrencyTransferFromAccountUpdater updater;

        public CurrencyTransferFromAccountDuplicateImporter(
            ICurrencyTransferFromAccountReader reader,
            ICurrencyTransferFromAccountUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = CurrencyTransferFromAccountMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.ProvideInAccounting = true;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
