using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(IRefundToSettlementAccountReader))]
    internal sealed class RefundToSettlementAccountReader : IRefundToSettlementAccountReader
    {
        private readonly RefundToSettlementAccountApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly RefundToSettlementAccountLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public RefundToSettlementAccountReader(
            RefundToSettlementAccountApiClient apiClient,
            IKontragentsReader kontragentsReader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            RefundToSettlementAccountLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<RefundToSettlementAccountResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.Contractor?.Type == ContractorType.Kontragent && response.Contractor.Id > 0)
            {
                var kontragent = await kontragentsReader.GetByIdAsync(response.Contractor.Id);
                response.Contractor.Form = (int?)kontragent?.Form;
            }
            response.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(response.Date.Year);
            var documents = await linksGetter.GetAsync(documentBaseId);
            // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
            //response.Bills = documents.Bills;
            response.Contract = documents.Contract;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
