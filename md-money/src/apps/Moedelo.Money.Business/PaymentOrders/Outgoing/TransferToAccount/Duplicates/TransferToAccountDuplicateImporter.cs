using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingTransferToAccount)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class TransferToAccountDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ITransferToAccountReader reader;
        private readonly ITransferToAccountUpdater updater;

        public TransferToAccountDuplicateImporter(
            ITransferToAccountReader reader,
            ITransferToAccountUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = TransferToAccountMapper.MapToSaveRequest(response);
            saveRequest.IsPaid = true;
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
