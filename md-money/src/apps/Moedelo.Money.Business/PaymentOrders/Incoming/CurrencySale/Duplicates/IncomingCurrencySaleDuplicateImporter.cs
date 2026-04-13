using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencySale)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class IncomingCurrencySaleDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IIncomingCurrencySaleReader reader;
        private readonly IIncomingCurrencySaleUpdater updater;

        public IncomingCurrencySaleDuplicateImporter(
            IIncomingCurrencySaleReader reader,
            IIncomingCurrencySaleUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = IncomingCurrencySaleMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            saveRequest.ProvideInAccounting = true;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
