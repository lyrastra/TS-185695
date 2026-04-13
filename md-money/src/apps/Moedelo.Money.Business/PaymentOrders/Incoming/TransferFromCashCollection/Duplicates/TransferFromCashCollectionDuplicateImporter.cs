using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCashCollection.Duplicates
{
    [OperationType(OperationType.MemorialWarrantTransferFromCashCollection)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class TransferFromCashCollectionDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ITransferFromCashCollectionReader reader;
        private readonly ITransferFromCashCollectionUpdater updater;

        public TransferFromCashCollectionDuplicateImporter(
            ITransferFromCashCollectionReader reader,
            ITransferFromCashCollectionUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = TransferFromCashCollectionMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
