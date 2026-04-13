using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentKbkService))]
    internal sealed class UnifiedBudgetaryPaymentKbkService : IUnifiedBudgetaryPaymentKbkService
    {
        private readonly IFirmRequisitesReader requisitesReader;
        private readonly IKbkReader kbkReader;

        public UnifiedBudgetaryPaymentKbkService(
            IFirmRequisitesReader requisitesReader,
            IKbkReader kbkReader)
        {
            this.requisitesReader = requisitesReader;
            this.kbkReader = kbkReader;
        }

        public async Task<BudgetaryKbkResponse[]> KbkAutocompleteAsync(BudgetaryKbkRequest request)
        {
            var isOoo = await requisitesReader.IsOooAsync();

            var kbkByAccountCode = await kbkReader.GetKbkByAccountCodeAsync(request, isOoo);

            return kbkByAccountCode
                .OrderBy(x => x.KbkPaymentType)
                .ThenBy(x => x.Id)
                .Select(Map)
                .ToArray();
        }

        private static BudgetaryKbkResponse Map(Kbk x)
        {
            return new BudgetaryKbkResponse
            {
                Id = x.Id,
                IsForIp = x.KbkUsingType == KbkUsingType.Ip,
                Name = $"{x.Description} КБК {x.Number}",
                SubcontoId = x.SubcontoId
            };
        }
    }
}
