using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.PaymentOrders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentAccountsReader))]
    class UnifiedBudgetaryPaymentAccountsReader : IUnifiedBudgetaryPaymentAccountsReader
    {
        private static readonly IReadOnlyCollection<UnifiedBudgetaryAccountCodes> AccountCodesForOoo = new[]
        {
            UnifiedBudgetaryAccountCodes.TransportTaxes,
            UnifiedBudgetaryAccountCodes.PropertyTaxes,
            UnifiedBudgetaryAccountCodes.ProfitTaxes,
        };

        private static readonly IReadOnlyCollection<UnifiedBudgetaryAccountCodes> AccountCodes = new[]
        {
            UnifiedBudgetaryAccountCodes.Ndfl,
            UnifiedBudgetaryAccountCodes.Nds,
            UnifiedBudgetaryAccountCodes.TouristTaxes,
            UnifiedBudgetaryAccountCodes.TradingFees,
            UnifiedBudgetaryAccountCodes.Eshn,
            UnifiedBudgetaryAccountCodes.EnvdForUsn,
            UnifiedBudgetaryAccountCodes.LandTaxes,
            UnifiedBudgetaryAccountCodes.Patent,
            UnifiedBudgetaryAccountCodes.InsuranceFee
        };

        private static readonly IReadOnlyDictionary<UnifiedBudgetaryAccountCodes, BudgetaryPeriodType> AccountCodeToPeriodType = new Dictionary<UnifiedBudgetaryAccountCodes, BudgetaryPeriodType>()
        {
            { UnifiedBudgetaryAccountCodes.Nds, BudgetaryPeriodType.Quarter },
            { UnifiedBudgetaryAccountCodes.ProfitTaxes, BudgetaryPeriodType.Quarter },
            { UnifiedBudgetaryAccountCodes.TransportTaxes, BudgetaryPeriodType.Quarter },
            { UnifiedBudgetaryAccountCodes.EnvdForUsn, BudgetaryPeriodType.Quarter },
            { UnifiedBudgetaryAccountCodes.LandTaxes, BudgetaryPeriodType.Quarter },
            { UnifiedBudgetaryAccountCodes.TradingFees, BudgetaryPeriodType.Quarter }
        };

        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IFirmRequisitesReader requisitesReader;
        private readonly ISyntheticAccountClient syntheticAccountClient;

        public UnifiedBudgetaryPaymentAccountsReader(
            IExecutionInfoContextAccessor contextAccessor,
            IFirmRequisitesReader requisitesReader,
            ISyntheticAccountClient syntheticAccountClient)
        {
            this.contextAccessor = contextAccessor;
            this.requisitesReader = requisitesReader;
            this.syntheticAccountClient = syntheticAccountClient;
        }

        public async Task<IReadOnlyCollection<BudgetaryAccount>> GetAsync()
        {
            var isOoo = await requisitesReader.IsOooAsync();
            var codes = GetAvailableCodes(isOoo).Select(x => (SyntheticAccountCode)x).ToArray();

            var context = contextAccessor.ExecutionInfoContext;
            var syntheticAccounts = await syntheticAccountClient.GetByCodes(codes);
            return syntheticAccounts.Select(Map).OrderBy(x => x.Code).ToArray();
        }

        private static IReadOnlyCollection<UnifiedBudgetaryAccountCodes> GetAvailableCodes(bool isOoo)
        {
            return isOoo
                ? AccountCodes.Concat(AccountCodesForOoo).ToArray()
                : AccountCodes;
        }

        private static BudgetaryAccount Map(SyntheticAccountDto account)
        {

            return new BudgetaryAccount
            {
                Code = (BudgetaryAccountCodes)account.Code,
                Name = GetName(account),
                FullNumber = account.Code.GetAccountDisplayName(),
                DefaultPeriodType = GetByAccount((UnifiedBudgetaryAccountCodes)account.Code)
            };
        }

        private static string GetName(SyntheticAccountDto account)
        {
            var result = account.Name;
            // DEV-1761 - Для целей бюджетного платежа изменить название
            if ((BudgetaryAccountCodes)account.Code == BudgetaryAccountCodes.ProfitTaxes)
            {
                result = "Налог на прибыль (расчеты с бюджетом)";
            }
            if ((BudgetaryAccountCodes)account.Code == BudgetaryAccountCodes.UnifiedBudgetaryPayment)
            {
                result = "ЕНП (Единый налоговый платёж)";
            }

            return result;
        }

        public static BudgetaryPeriodType GetByAccount(UnifiedBudgetaryAccountCodes account)
        {
            return AccountCodeToPeriodType.TryGetValue(account, out var period)
                ? period
                : BudgetaryPeriodType.Month;
        }
    }
}
