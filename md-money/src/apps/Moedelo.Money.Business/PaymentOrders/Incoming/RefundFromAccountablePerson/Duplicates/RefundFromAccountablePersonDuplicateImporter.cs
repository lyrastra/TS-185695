using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingRefundFromAccountablePerson)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class RefundFromAccountablePersonDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IRefundFromAccountablePersonReader reader;
        private readonly IRefundFromAccountablePersonUpdater updater;

        public RefundFromAccountablePersonDuplicateImporter(
            IRefundFromAccountablePersonReader reader,
            IRefundFromAccountablePersonUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = RefundFromAccountablePersonMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
