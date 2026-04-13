using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [InjectAsSingleton(typeof(CurrencyOtherOutgoingProvideInAccountingFixer))]
    internal sealed class CurrencyOtherOutgoingProvideInAccountingFixer
    {
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public CurrencyOtherOutgoingProvideInAccountingFixer(
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task FixAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            if (!request.ProvideInAccounting)
            {
                var isOoo = await firmRequisitesReader.IsOooAsync();
                request.ProvideInAccounting = !isOoo;
            }
        }
    }
}
