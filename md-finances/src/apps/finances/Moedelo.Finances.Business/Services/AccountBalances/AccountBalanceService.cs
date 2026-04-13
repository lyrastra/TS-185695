using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountManagement.Client.SharedFirms;
using Moedelo.AccountManagement.Dto.SharedFirms;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.AccountBalances;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Models.AccountBalances;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.RequisitesV2.Client.SettlementAccounts;

namespace Moedelo.Finances.Business.Services.AccountBalances
{
    [InjectAsSingleton]
    public class AccountBalanceService : IAccountBalanceService
    {
        private readonly ISharedFirmsApiClient sharedFirmsApiClient;
        
        private readonly ISettlementAccountClient settlementAccountClient;

        private readonly IBanksApiClient banksApiClient;

        private readonly IAccMoneyBalancesReader accMoneyBalancesReader;

        public AccountBalanceService(
            ISharedFirmsApiClient sharedFirmsApiClient,
            ISettlementAccountClient settlementAccountClient,
            IBanksApiClient banksApiClient, 
            IAccMoneyBalancesReader accMoneyBalancesReader)
        {
            this.sharedFirmsApiClient = sharedFirmsApiClient;
            this.settlementAccountClient = settlementAccountClient;
            this.banksApiClient = banksApiClient;
            this.accMoneyBalancesReader = accMoneyBalancesReader;
        }

        public async Task<List<FirmSettlementAccountBalance>> GetFirmSettlementAccountBalanceAsync(IUserContext context,
            CancellationToken cancellationToken)
        {
            var sharedFirms = await sharedFirmsApiClient.GetFirmsInAccountAsync(context.UserId, context.FirmId)
                .ConfigureAwait(false);
            var firmSettlementAccountBalanceList = await sharedFirms
                .SelectParallelAsync((sf, ctx) => GetFirmSettlementAccountBalanceAsync(context.UserId, sf, ctx),
                    maxDegreeOfParallelism: 10, cancellationToken)
                .ConfigureAwait(false);

            var result = firmSettlementAccountBalanceList
                .SelectMany(list => list)
                .ToList();

            var banksIds = result.Select(r => r.SettlementAccountBankId).Distinct().ToList();
            var banks = await banksApiClient.GetByIdsAsync(banksIds, cancellationToken).ConfigureAwait(false);

            foreach (var fsab in result)
            {
                var bank = banks.FirstOrDefault(b => b.Id == fsab.SettlementAccountBankId);

                if (bank != null)
                {
                    fsab.SettlementAccountBankName = bank.Name;
                }
            }

            return result;
        }

        private async Task<List<FirmSettlementAccountBalance>> GetFirmSettlementAccountBalanceAsync(int userId,
            FirmDto firm, CancellationToken ctx)
        {
            var firmSettlementsAccounts = await settlementAccountClient
                .GetAsync(firm.Id, userId, ctx)
                .ConfigureAwait(false);

            if (firmSettlementsAccounts.Count == 0)
            {
                return new List<FirmSettlementAccountBalance>(0);
            }

            var sources = firmSettlementsAccounts.Select(fsa => new MoneySourceBase
            {
                Id = fsa.Id,
                SubcontoId = fsa.SubcontoId,
                Type = MoneySourceType.SettlementAccount,
            }).ToList();
            var balances = sources.Count > 0
                ? await accMoneyBalancesReader
                    .GetAsync(firm.Id, userId, sources, cancellationToken: ctx)
                    .ConfigureAwait(false)
                : new List<MoneySourceBalance>();

            var result = firmSettlementsAccounts.Select(fsa => new FirmSettlementAccountBalance
            {
                FirmId = firm.Id,
                FirmName = firm.Name,
                SettlementAccountId = fsa.Id,
                SettlementAccountName = fsa.Name,
                SettlementAccountNumber = fsa.Number,
                SettlementAccountBankId = fsa.BankId,
                Balance = balances.FirstOrDefault(b => b.Id == fsa.Id)?.Balance ?? 0m,
            }).ToList();

            return result;
        }
    }
}