using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCashCollection.Duplicates
{
    [OperationType(OperationType.MemorialWarrantTransferFromCashCollection)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class TransferFromCashCollectionDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ITransferFromCashCollectionReader reader;
        private readonly ITransferFromCashCollectionUpdater updater;

        public TransferFromCashCollectionDuplicateMerger(
            ITransferFromCashCollectionReader reader,
            ITransferFromCashCollectionUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task MergeAsync(PaymentOrderDuplicateMergeRequest request)
        {
            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false);
            if (response.Date == request.Date)
            {
                return;
            }
            var saveRequest = TransferFromCashCollectionMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
