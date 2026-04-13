using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingRefundFromAccountablePerson)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class RefundFromAccountablePersonDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IRefundFromAccountablePersonReader reader;
        private readonly IRefundFromAccountablePersonUpdater updater;

        public RefundFromAccountablePersonDuplicateMerger(
            IRefundFromAccountablePersonReader reader,
            IRefundFromAccountablePersonUpdater updater)
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
            var saveRequest = RefundFromAccountablePersonMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
