using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums.CashOrders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.CashOrders;

namespace Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentReader))]
    class UnifiedBudgetaryPaymentReader : IUnifiedBudgetaryPaymentReader
    {
        private readonly UnifiedBudgetaryPaymentApiClient apiClient;
        private readonly IKbkReader kbkReader;
        private readonly ICashOrderAccessor cashOrderAccessor;

        public UnifiedBudgetaryPaymentReader(
            UnifiedBudgetaryPaymentApiClient apiClient,
            IKbkReader kbkReader,
            ICashOrderAccessor cashOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kbkReader = kbkReader;
            this.cashOrderAccessor = cashOrderAccessor;
        }

        public async Task<UnifiedBudgetaryPaymentResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            await LoadKbkDataAsync(response);
            response.IsReadOnly = cashOrderAccessor.IsReadOnly(response.ProvideInAccounting);
            return response;
        }

        private async Task LoadKbkDataAsync(UnifiedBudgetaryPaymentResponse response)
        {
            var kbkIds = response.SubPayments
                .Select(x => x.Kbk.Id)
                .Distinct()
                .ToArray();
            var kbks = await kbkReader.GetByIdsAsync(kbkIds);
            var kbkMap = kbks.ToDictionary(x => x.Id);
            foreach (var subPayment in response.SubPayments)
            {
                var kbk = kbkMap.GetValueOrDefault(subPayment.Kbk.Id);
                subPayment.Kbk.Number = kbk?.Number;
                subPayment.Kbk.AccountCode = (UnifiedBudgetaryAccountCodes)(kbk?.AccountCode ?? 0);
            }
        }
    }
}
