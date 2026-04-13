using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.WithdrawalFromAccount.Duplicates
{
    [OperationType(OperationType.MemorialWarrantWithdrawalFromAccount)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class WithdrawalFromAccountDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IWithdrawalFromAccountReader reader;
        private readonly IWithdrawalFromAccountUpdater updater;

        public WithdrawalFromAccountDuplicateImporter(
            IWithdrawalFromAccountReader reader,
            IWithdrawalFromAccountUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = WithdrawalFromAccountMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
