using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingTransferFromPurse)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class TransferFromPurseDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ITransferFromPurseReader reader;
        private readonly ITransferFromPurseUpdater updater;

        public TransferFromPurseDuplicateImporter(
            ITransferFromPurseReader reader,
            ITransferFromPurseUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = TransferFromPurseMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
