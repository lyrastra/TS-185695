using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingTransferFromAccount)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class TransferFromAccountDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ITransferFromAccountReader reader;
        private readonly ITransferFromAccountUpdater updater;

        public TransferFromAccountDuplicateMerger(
            ITransferFromAccountReader reader,
            ITransferFromAccountUpdater updater)
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
            var saveRequest = TransferFromAccountMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
