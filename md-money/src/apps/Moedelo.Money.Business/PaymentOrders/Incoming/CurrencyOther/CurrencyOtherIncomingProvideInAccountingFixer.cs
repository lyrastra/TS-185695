using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther
{
    [InjectAsSingleton(typeof(CurrencyOtherIncomingProvideInAccountingFixer))]
    internal sealed class CurrencyOtherIncomingProvideInAccountingFixer
    {
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public CurrencyOtherIncomingProvideInAccountingFixer(
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task FixAsync(CurrencyOtherIncomingSaveRequest request)
        {
            if (!request.ProvideInAccounting)
            {
                var isOoo = await firmRequisitesReader.IsOooAsync();
                request.ProvideInAccounting = !isOoo;
            }
        }
    }
}