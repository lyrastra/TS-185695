using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentReader))]
    class UnifiedBudgetaryPaymentReader : IUnifiedBudgetaryPaymentReader
    {
        private readonly IUnifiedBudgetaryPaymentApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;
        private readonly IUnifiedBudgetaryPaymentLinksHelper linksHelper;
        private readonly IKbkReader kbkReader;


        public UnifiedBudgetaryPaymentReader(
            IUnifiedBudgetaryPaymentApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor,
            IUnifiedBudgetaryPaymentLinksHelper linksHelper,
            IKbkReader kbkReader)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
            this.linksHelper = linksHelper;
            this.kbkReader = kbkReader;
        }

        public async Task<UnifiedBudgetaryPaymentResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var savedModel = await apiClient.GetAsync(documentBaseId);
            await FillSavedModelAsync(savedModel);
            return savedModel;
        }

        public async Task<UnifiedBudgetaryPaymentResponse> GetCopyByBaseIdAsync(long documentBaseId)
        {
            var savedModel = await apiClient.GetAsync(documentBaseId);
            await FillSavedModelAsync(savedModel);
            return savedModel;
        }

        private async Task FillSavedModelAsync(UnifiedBudgetaryPaymentResponse model)
        {
            await FillKbkInfo(model);
            await linksHelper.FillModelAsync(model);
            model.IsReadOnly = paymentOrderAccessor.IsReadOnly(model);
        }

        private async Task FillKbkInfo(UnifiedBudgetaryPaymentResponse model)
        {
            var kbkIds = model.SubPayments.Select(k => k.Kbk.Id).ToList();
            kbkIds.Add(model.KbkId);

            var kbks = await kbkReader.GetByIdsAsync(kbkIds);

            var mainkbk = kbks.FirstOrDefault(kbk => kbk.Id == model.KbkId);
            model.KbkId = mainkbk.Id;
            model.KbkNumber = mainkbk.Number;
            model.AccountCode = mainkbk.AccountCode;

            foreach (var subPayment in model.SubPayments)
            {
                var kbk = kbks.FirstOrDefault(kbk => kbk.Id == subPayment.Kbk.Id);
                if (kbk == null) continue;
                subPayment.Kbk.Number = kbk.Number;
                subPayment.Kbk.KbkPaymentType = kbk.KbkPaymentType;
                subPayment.Kbk.AccountCode = kbk.AccountCode;
            }
        }
    }
}
