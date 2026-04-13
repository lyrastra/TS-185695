using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash.Duplicates
{
    [OperationType(OperationType.MemorialWarrantTransferFromCash)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class TransferFromCashDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ITransferFromCashReader reader;
        private readonly ITransferFromCashUpdater updater;

        public TransferFromCashDuplicateImporter(
            ITransferFromCashReader reader,
            ITransferFromCashUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = TransferFromCashMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
