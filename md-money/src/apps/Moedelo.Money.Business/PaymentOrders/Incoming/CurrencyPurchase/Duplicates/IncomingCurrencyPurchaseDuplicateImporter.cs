using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyPurchase)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class IncomingCurrencyPurchaseDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IIncomingCurrencyPurchaseReader reader;
        private readonly IIncomingCurrencyPurchaseUpdater updater;

        public IncomingCurrencyPurchaseDuplicateImporter(
            IIncomingCurrencyPurchaseReader reader,
            IIncomingCurrencyPurchaseUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = IncomingCurrencyPurchaseMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
