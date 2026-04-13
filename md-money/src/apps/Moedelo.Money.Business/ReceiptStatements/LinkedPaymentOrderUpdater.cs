using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.ClosedPeriod;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions;

namespace Moedelo.Money.Business.ReceiptStatements
{
    [InjectAsSingleton(typeof(ILinkedPaymentOrderUpdater))]
    internal sealed class LinkedPaymentOrderUpdater : ILinkedPaymentOrderUpdater
    {
        private readonly PaymentToSupplierApiClient apiClient;
        private readonly IClosedPeriodReader closedPeriodReader;
        private readonly IPaymentToSupplierProvider provider;

        public LinkedPaymentOrderUpdater(
            PaymentToSupplierApiClient apiClient,
            IClosedPeriodReader closedPeriodReader,
            IPaymentToSupplierProvider paymentToSupplierProvider)
        {
            this.apiClient = apiClient;
            this.closedPeriodReader = closedPeriodReader;
            this.provider = paymentToSupplierProvider;
        }

        public async Task UpdatePaymentsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (!documentBaseIds.Any())
            {
                return;
            }

            var payments = await documentBaseIds.SelectParallelAsync(apiClient.GetAsync);

            var closedPeriod = await closedPeriodReader.GetLastClosedDateAsync();

            // выбираем платежные документы из открытого периода, в которых указан Основной контрагент
            var paymentsForUpdate = payments.Where(x => x.IsMainContractor && x.Date > closedPeriod).ToArray();
            if (paymentsForUpdate.Length <= 0)
            {
                return;
            }

            // платежные поручения специально обновляются через апи-клиент,
            // чтобы избежать выбрасывание события на обновление п/п
            // есть вероятность, что таким образом можно зациклиться через кафку:
            // Money.PaymentToSupplierUpdated -> LinkedDocuments.ReceiptStatementAndPaymentChangeLinks ->
            // Money.PaymentToSupplierUpdated  -> ...
            var saveRequests = paymentsForUpdate.Select(PaymentToSupplierMapper.MapToSaveRequest).ToArray();
            foreach (var saveRequest in saveRequests)
            {
                saveRequest.IsMainContractor = false;
            }

            var documentBaseIdsForProvide = saveRequests.Select(x => x.DocumentBaseId).ToArray();

            await saveRequests.RunParallelAsync(apiClient.UpdateAsync);
            await documentBaseIdsForProvide.RunParallelAsync(provider.ProvideAsync);
        }
    }
}
