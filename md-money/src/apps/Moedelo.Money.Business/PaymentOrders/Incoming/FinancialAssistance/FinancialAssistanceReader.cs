using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Incoming.FinancialAssistance;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.FinancialAssistance
{
    [InjectAsSingleton(typeof(IFinancialAssistanceReader))]
    internal sealed class FinancialAssistanceReader : IFinancialAssistanceReader
    {
        private readonly FinancialAssistanceApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public FinancialAssistanceReader(
            FinancialAssistanceApiClient apiClient,
            IKontragentsReader kontragentsReader,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<FinancialAssistanceResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id).ConfigureAwait(false);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            return response;
        }
    }
}
