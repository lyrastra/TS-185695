using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash.Duplicates
{
    [OperationType(OperationType.MemorialWarrantTransferFromCash)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class TransferFromCashDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ITransferFromCashReader reader;
        private readonly ITransferFromCashUpdater updater;

        public TransferFromCashDuplicateMerger(
            ITransferFromCashReader reader,
            ITransferFromCashUpdater updater)
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
            var saveRequest = TransferFromCashMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
