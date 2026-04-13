using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Generators
{
    [InjectAsSingleton(typeof(SnapshotFixer))]
    internal class SnapshotFixer
    {
        private readonly IBankApiClient bankClient;

        public SnapshotFixer(IBankApiClient bankClient)
        {
            this.bankClient = bankClient;
        }

        public async Task FixBankInfoAsync(OrderDetails details, PaymentOrderSaveRequest request)
        {
            if (string.IsNullOrEmpty(request.KontragentRequisites?.BankBik))
            {
                return;
            }
            var bank = await bankClient.GetByBikAsync(request.KontragentRequisites.BankBik).ConfigureAwait(true);
            if (bank == null)
            {
                return;
            }
            details.BankName = bank.FullNameWithCity;
            details.BankCity = bank.City;
            details.BankCorrespondentAccount = bank.CorrespondentAccount;
        }

        public async Task FixBudgetaryPaymentBankInfoAsync(OrderDetails details, PaymentOrderSaveRequest request)
        {
            if (string.IsNullOrEmpty(request.KontragentRequisites?.BankBik))
            {
                return;
            }
            var bank = await bankClient.GetByBikAsync(request.KontragentRequisites.BankBik).ConfigureAwait(true);
            if (bank == null)
            {
                return;
            }
            details.BankName = bank.FullNameWithCity;
            details.BankCity = bank.City;
        }
    }
}
