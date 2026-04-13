using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingTransferFromPurse)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class TransferFromPurseDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ITransferFromPurseReader reader;
        private readonly ITransferFromPurseUpdater updater;

        public TransferFromPurseDuplicateMerger(
            ITransferFromPurseReader reader,
            ITransferFromPurseUpdater updater)
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
            var saveRequest = TransferFromPurseMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
