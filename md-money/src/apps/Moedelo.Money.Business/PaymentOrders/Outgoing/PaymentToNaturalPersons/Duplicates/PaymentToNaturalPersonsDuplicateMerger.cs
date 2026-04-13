using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToNaturalPersons)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class PaymentToNaturalPersonsDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IPaymentToNaturalPersonsReader reader;
        private readonly IPaymentToNaturalPersonsUpdater updater;

        public PaymentToNaturalPersonsDuplicateMerger(
            IPaymentToNaturalPersonsReader reader,
            IPaymentToNaturalPersonsUpdater updater)
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
            var saveRequest = PaymentToNaturalPersonsMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
