using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    [InjectAsSingleton(typeof(IRefundToCustomerReader))]
    class RefundToCustomerReader : IRefundToCustomerReader
    {
        private readonly RefundToCustomerApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly RefundToCustomerLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public RefundToCustomerReader(
            RefundToCustomerApiClient apiClient,
            IKontragentsReader kontragentsReader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            RefundToCustomerLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<RefundToCustomerResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            if (response.TaxationSystemType == null)
            {
                response.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(response.Date.Year);
            }
            var documents = await linksGetter.GetAsync(documentBaseId);
            response.Contract = documents.Contract;
            response.RetailRefund = documents.RetailRefund;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);

            return response;
        }
    }
}
