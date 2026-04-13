using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToNaturalPersons)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class PaymentToNaturalPersonsDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IPaymentToNaturalPersonsReader reader;
        private readonly IPaymentToNaturalPersonsUpdater updater;

        public PaymentToNaturalPersonsDuplicateImporter(
            IPaymentToNaturalPersonsReader reader,
            IPaymentToNaturalPersonsUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = PaymentToNaturalPersonsMapper.MapToSaveRequest(response);
            saveRequest.IsPaid = true;
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
