using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToAccountablePerson)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class PaymentToAccountablePersonDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IPaymentToAccountablePersonReader reader;
        private readonly IPaymentToAccountablePersonUpdater updater;

        public PaymentToAccountablePersonDuplicateMerger(
            IPaymentToAccountablePersonReader reader,
            IPaymentToAccountablePersonUpdater updater)
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
            var saveRequest = PaymentToAccountablePersonMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
