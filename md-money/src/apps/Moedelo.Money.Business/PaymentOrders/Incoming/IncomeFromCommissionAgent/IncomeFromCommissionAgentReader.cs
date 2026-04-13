using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    [InjectAsSingleton(typeof(IIncomeFromCommissionAgentReader))]
    internal sealed class IncomeFromCommissionAgentReader : IIncomeFromCommissionAgentReader
    {
        private readonly IncomeFromCommissionAgentApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly IncomeFromCommissionAgentLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public IncomeFromCommissionAgentReader(
            IncomeFromCommissionAgentApiClient apiClient,
            IKontragentsReader kontragentsReader,
            IncomeFromCommissionAgentLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<IncomeFromCommissionAgentResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            var documents = await linksGetter.GetAsync(documentBaseId);
            response.Contract = documents.Contract;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
