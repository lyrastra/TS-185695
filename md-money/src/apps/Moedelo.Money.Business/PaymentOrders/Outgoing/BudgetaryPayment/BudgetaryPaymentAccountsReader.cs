using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.UnifiedBudgetaryPayments;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.UnifiedBudgetaryPayments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(BudgetaryPaymentAccountsReader))]
    class BudgetaryPaymentAccountsReader
    {
        private static readonly IReadOnlyCollection<BudgetaryAccountCodes> AccountCodesForOoo = new[]
        {
            BudgetaryAccountCodes.TransportTaxes,
            BudgetaryAccountCodes.PropertyTaxes,
            BudgetaryAccountCodes.ProfitTaxes,
        };

        private static readonly IReadOnlyCollection<BudgetaryAccountCodes> AccountCodesAfter2023 = new[]
        {
            BudgetaryAccountCodes.UnifiedBudgetaryPayment,
            BudgetaryAccountCodes.Ndfl,
            BudgetaryAccountCodes.OtherTaxes,
            BudgetaryAccountCodes.FssInjuryFee
        };

        private static readonly IReadOnlyCollection<BudgetaryAccountCodes> AccountCodesBefore2023 = new[]
        {
            BudgetaryAccountCodes.Ndfl,
            BudgetaryAccountCodes.OtherTaxes,
            BudgetaryAccountCodes.FssInjuryFee,
            BudgetaryAccountCodes.Nds,
            BudgetaryAccountCodes.TradingFees,
            BudgetaryAccountCodes.Eshn,
            BudgetaryAccountCodes.EnvdForUsn,
            BudgetaryAccountCodes.LandTaxes,
            BudgetaryAccountCodes.FssFee,
            BudgetaryAccountCodes.PfrInsuranceFee,
            BudgetaryAccountCodes.PfrAccumulateFee,
            BudgetaryAccountCodes.FomsFee,
            BudgetaryAccountCodes.Patent
        };

        private static readonly IReadOnlyDictionary<BudgetaryAccountCodes, BudgetaryPeriodType> AccountCodeToPeriodType = new Dictionary<BudgetaryAccountCodes, BudgetaryPeriodType>
        {
            { BudgetaryAccountCodes.Nds, BudgetaryPeriodType.Quarter },
            { BudgetaryAccountCodes.ProfitTaxes, BudgetaryPeriodType.Quarter },
            { BudgetaryAccountCodes.TransportTaxes, BudgetaryPeriodType.Quarter },
            { BudgetaryAccountCodes.Eshn, BudgetaryPeriodType.Quarter },
            { BudgetaryAccountCodes.EnvdForUsn, BudgetaryPeriodType.Quarter },
            { BudgetaryAccountCodes.LandTaxes, BudgetaryPeriodType.Quarter },
            { BudgetaryAccountCodes.TradingFees, BudgetaryPeriodType.Quarter }
        };

        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IFirmRequisitesReader requisitesReader;
        private readonly ISyntheticAccountClient syntheticAccountClient;
        private readonly IUnifiedBudgetaryPaymentsLaunchService enpLaunchService;

        public BudgetaryPaymentAccountsReader(
            IExecutionInfoContextAccessor contextAccessor,
            IFirmRequisitesReader requisitesReader,
            ISyntheticAccountClient syntheticAccountClient,
            IUnifiedBudgetaryPaymentsLaunchService enpLaunchService)
        {
            this.contextAccessor = contextAccessor;
            this.requisitesReader = requisitesReader;
            this.syntheticAccountClient = syntheticAccountClient;
            this.enpLaunchService = enpLaunchService;
        }

        public async Task<IReadOnlyCollection<BudgetaryAccount>> GetAsync(DateTime paymentDate)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var isEnpEnabled = await enpLaunchService.IsEnpEnabledAsync(paymentDate);
            var isOoo = await requisitesReader.IsOooAsync();

            var availableCodes = GetAvailableCodes(isOoo, isEnpEnabled)
                .Select(x => (SyntheticAccountCode)x)
                .ToArray();
            var syntheticAccounts = await syntheticAccountClient.GetByCodes(availableCodes);

            return isEnpEnabled
                ? syntheticAccounts.Select(Map).OrderBy(x => x.Name).ToArray()
                : syntheticAccounts.Select(Map).OrderBy(x => x.Code).ToArray();
        }

        private static IReadOnlyCollection<BudgetaryAccountCodes> GetAvailableCodes(bool isOoo, bool isEnpEnabled)
        {
            if (isEnpEnabled)
            {
                return AccountCodesAfter2023;
            }

            return isOoo
                ? AccountCodesBefore2023.Concat(AccountCodesForOoo).ToArray()
                : AccountCodesBefore2023;
        }

        private static BudgetaryAccount Map(SyntheticAccountDto account)
        {

            return new BudgetaryAccount
            {
                Code = (BudgetaryAccountCodes)account.Code,
                Name = GetName(account),
                FullNumber = account.Code.GetAccountDisplayName(),
                DefaultPeriodType = GetByAccount((BudgetaryAccountCodes)account.Code)
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

        public static BudgetaryPeriodType GetByAccount(BudgetaryAccountCodes account)
        {
            return AccountCodeToPeriodType.TryGetValue(account, out var period)
                ? period
                : BudgetaryPeriodType.Month;
        }
    }
}
