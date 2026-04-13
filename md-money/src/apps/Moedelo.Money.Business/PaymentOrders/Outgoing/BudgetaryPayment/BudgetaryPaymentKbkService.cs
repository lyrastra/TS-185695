using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Abstractions.UnifiedBudgetaryPayments;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentKbkService))]
    internal sealed class BudgetaryPaymentKbkService : IBudgetaryPaymentKbkService
    {
        private readonly IFirmRequisitesReader requisitesReader;
        private readonly IKbkReader kbkReader;
        private readonly IUnifiedBudgetaryPaymentsLaunchService enpLaunchService;

        public BudgetaryPaymentKbkService(
            IFirmRequisitesReader requisitesReader,
            IKbkReader kbkReader,
            IUnifiedBudgetaryPaymentsLaunchService enpLaunchService)
        {
            this.requisitesReader = requisitesReader;
            this.kbkReader = kbkReader;
            this.enpLaunchService = enpLaunchService;
        }

        public async Task<BudgetaryKbkResponse[]> KbkAutocompleteAsync(BudgetaryKbkRequest request)
        {
            var isOoo = await requisitesReader.IsOooAsync();

            var kbkByAccountCode = await kbkReader.GetKbkByAccountCodeAsync(request, isOoo);
            if (request.AccountCode == BudgetaryAccountCodes.EnvdForUsn)
            {
                kbkByAccountCode = kbkByAccountCode.OrderBy(x => x.KbkType == KbkType.DeclarationUsnProfitOutgoMinTax).ToArray();
            }

            var enpStartDate = await enpLaunchService.GetEnpStartDateAsync();
            var isEnpEnabledAsync = request.Date >= enpStartDate;

            return kbkByAccountCode
                .Where(x => FilterKbk(x, isEnpEnabledAsync))
                .Select(x => new BudgetaryKbkResponse
                {
                    Id = x.Id,
                    IsForIp = x.KbkUsingType == KbkUsingType.Ip,
                    Name = $"{x.Description} КБК {x.Number}",
                    SubcontoId = x.SubcontoId
                }).ToArray();
        }

        private static bool FilterKbk(Kbk kbk, bool isEnpEnabledAsync)
        {
            // RS-2054 Не показываем эти кбк для БП в 2023 году
            if (kbk.KbkType == KbkType.NdflForIp &&
                isEnpEnabledAsync)
            {
                return false;
            }
            return true;
        }
    }
}
