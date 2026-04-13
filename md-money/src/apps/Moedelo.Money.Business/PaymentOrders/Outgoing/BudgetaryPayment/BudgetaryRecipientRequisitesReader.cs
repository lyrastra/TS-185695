using Moedelo.Authorities.ApiClient.Abstractions.Sfr;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System.Threading.Tasks;
using IBankApiClient = Moedelo.Catalog.ApiClient.Abstractions.legacy.IBankApiClient;
using ICatalogFnsApiClient = Moedelo.Catalog.ApiClient.Abstractions.legacy.IFnsApiClient;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(BudgetaryRecipientRequisitesReader))]
    class BudgetaryRecipientRequisitesReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IFnsApiClient fnsApiClient;
        private readonly IFssApiClient fssApiClient;
        private readonly IPfrApiClient pfrApiClient;
        private readonly IBankApiClient bankClient;
        private readonly ICatalogFnsApiClient catalogFnsClient;
        private readonly ISfrFirmRequisitesApiClient sfrFirmRequisitesApiClient;

        public BudgetaryRecipientRequisitesReader(
            IExecutionInfoContextAccessor contextAccessor,
            IFnsApiClient fnsApiClient,
            IFssApiClient fssApiClient,
            IPfrApiClient pfrApiClient,
            IBankApiClient bankClient,
            ICatalogFnsApiClient catalogFnsClient,
            ISfrFirmRequisitesApiClient sfrFirmRequisitesApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.fnsApiClient = fnsApiClient;
            this.fssApiClient = fssApiClient;
            this.pfrApiClient = pfrApiClient;
            this.bankClient = bankClient;
            this.catalogFnsClient = catalogFnsClient;
            this.sfrFirmRequisitesApiClient = sfrFirmRequisitesApiClient;
        }

        public Task<BudgetaryRecipient> GetByFundTypeAsync(BudgetaryFundType fundType)
        {
            return fundType switch
            {
                BudgetaryFundType.Fns => GetFnsDepartmentRequisitesAsync(),
                BudgetaryFundType.Pfr => GetPfrDepartmentRequisitesAsync(),
                BudgetaryFundType.Sfr => GetSfrDepartmentRequisitesAsync(),

                BudgetaryFundType.FssDisability or
                BudgetaryFundType.FssInjury => GetFssDepartmentRequisitesAsync(),

                _ => Task.FromResult(new BudgetaryRecipient()),
            };
        }

        public async Task<BudgetaryRecipient> GetFnsRequisitesByCodeAndOktmoAsync(string code, string oktmo)
        {
            var fnsRequisites = await catalogFnsClient.GetRequisitesByCodeAndOktmoAsync(code, oktmo);
            var bank = await bankClient.GetByBikAsync(fnsRequisites.Bik);
            return new BudgetaryRecipient
            {
                Name = fnsRequisites.Recipient,
                Inn = fnsRequisites.Inn,
                Kpp = fnsRequisites.Kpp,
                SettlementAccount = fnsRequisites.SettlementAccount,
                BankBik = fnsRequisites.Bik,
                BankName = bank?.FullName,
                UnifiedSettlementAccount = fnsRequisites?.UnifiedSettlementAccount,
                Oktmo = oktmo
            };
        }

        public async Task<BudgetaryRecipient> GetUnifiedBudgetaryPaymentFnsDepartmentRequisitesAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var fnsDepartment = await fnsApiClient.GetUnifiedBudgetaryPaymentDepartmentAsync(context.FirmId, context.UserId);
            var bank = await bankClient.GetByBikAsync(fnsDepartment.Bik);
            return new BudgetaryRecipient
            {
                Name = fnsDepartment.RecipientName,
                Inn = fnsDepartment.Inn,
                Kpp = fnsDepartment.Kpp,
                SettlementAccount = fnsDepartment.SettlementAccount,
                BankBik = fnsDepartment.Bik,
                BankName = bank?.FullName,
                UnifiedSettlementAccount = fnsDepartment?.UnifiedSettlementAccount,
                Okato = fnsDepartment.Okato,
                Oktmo = fnsDepartment.Oktmo
            };
        }

        private async Task<BudgetaryRecipient> GetFnsDepartmentRequisitesAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var fnsDepartment = await fnsApiClient.GetDepartmentAsync(context.FirmId, context.UserId);
            var bank = await bankClient.GetByBikAsync(fnsDepartment.Bik);
            return new BudgetaryRecipient
            {
                Name = fnsDepartment.RecipientName,
                Inn = fnsDepartment.Inn,
                Kpp = fnsDepartment.Kpp,
                SettlementAccount = fnsDepartment.SettlementAccount,
                BankBik = fnsDepartment.Bik,
                BankName = bank?.FullName,
                UnifiedSettlementAccount = fnsDepartment?.UnifiedSettlementAccount,
                Okato = fnsDepartment.Okato,
                Oktmo = fnsDepartment.Oktmo
            };
        }
        private async Task<BudgetaryRecipient> GetFssDepartmentRequisitesAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var fssDepartment = await fssApiClient.GetDepartmentAsync(context.FirmId, context.UserId);
            var bank = await bankClient.GetByBikAsync(fssDepartment.Bik);
            return new BudgetaryRecipient
            {
                Name = fssDepartment.RecipientName,
                Inn = fssDepartment.Inn,
                Kpp = fssDepartment.Kpp,
                SettlementAccount = fssDepartment.SettlementAccount,
                BankBik = fssDepartment.Bik,
                BankName = bank?.FullName,
                UnifiedSettlementAccount = fssDepartment?.UnifiedSettlementAccount,
                Okato = fssDepartment.Okato,
                Oktmo = fssDepartment.Oktmo
            };
        }

        private async Task<BudgetaryRecipient> GetPfrDepartmentRequisitesAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var pfrDepartment = await pfrApiClient.GetDepartmentAsync(context.FirmId, context.UserId);
            var bank = await bankClient.GetByBikAsync(pfrDepartment.Bik);
            return new BudgetaryRecipient
            {
                Name = pfrDepartment.RecipientName,
                Inn = pfrDepartment.Inn,
                Kpp = pfrDepartment.Kpp,
                SettlementAccount = pfrDepartment.SettlementAccount,
                BankBik = pfrDepartment.Bik,
                BankName = bank?.FullName,
                UnifiedSettlementAccount = pfrDepartment?.UnifiedSettlementAccount,
                Okato = pfrDepartment.Okato,
                Oktmo = pfrDepartment.Oktmo
            };
        }

        private async Task<BudgetaryRecipient> GetSfrDepartmentRequisitesAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var sfrDepartment = await sfrFirmRequisitesApiClient.GetAsync((int)context.FirmId, (int)context.UserId);
            var bank = await bankClient.GetByBikAsync(sfrDepartment.BankBik);
            return new BudgetaryRecipient
            {
                Name = sfrDepartment.Recipient,
                Inn = sfrDepartment.Inn,
                Kpp = sfrDepartment.Kpp,
                SettlementAccount = sfrDepartment.SettlementAccount,
                BankBik = sfrDepartment.BankBik,
                BankName = bank?.FullName,
                UnifiedSettlementAccount = sfrDepartment.UnifiedSettlementAccount,
                Oktmo = sfrDepartment.Oktmo
            };
        }
    }
}
