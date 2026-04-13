using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentReader))]
    class BudgetaryPaymentReader : IBudgetaryPaymentReader
    {
        private readonly BudgetaryPaymentApiClient apiClient;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;
        private readonly BudgetaryPaymentLinksGetter linksGetter;
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public BudgetaryPaymentReader(
            BudgetaryPaymentApiClient apiClient,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            IPaymentOrderAccessor paymentOrderAccessor,
            BudgetaryPaymentLinksGetter linksGetter,
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.apiClient = apiClient;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
            this.linksGetter = linksGetter;
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task<BudgetaryPaymentResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.TaxationSystemType == null)
            {
                response.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(response.Date.Year);
            }
            var links = await linksGetter.GetAsync(response);
            response.CurrencyInvoices = links.CurrencyInvoices;
            
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }

        public async Task<BudgetaryPaymentResponse> GetCopyByBaseIdAsync(long documentBaseId)
        {
            var response = await GetByBaseIdAsync(documentBaseId);
            var isOoo = await firmRequisitesReader.IsOooAsync();
            response.PayerStatus = response.PayerStatus.GetActualBudgetaryPayerStatus(isOoo);
            return response;
        }
    }
}
