using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.FirmFlags;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.UnifiedBudgetaryPayments;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.UnifiedBudgetaryPayments
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentsLaunchService))]
    class UnifiedBudgetaryPaymentsLaunchService : IUnifiedBudgetaryPaymentsLaunchService
    {
        private static readonly DateTime DefaultEnpStartDate = new(2023, 1, 1);

        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IFirmFlagsApiClient firmFlagsApiClient;
        private readonly SettingValue enpStartDateSetting;

        public UnifiedBudgetaryPaymentsLaunchService(
            IExecutionInfoContextAccessor contextAccessor,
            IFirmFlagsApiClient firmFlagsApiClient,
            ISettingRepository settingRepository)
        {
            this.contextAccessor = contextAccessor;
            this.firmFlagsApiClient = firmFlagsApiClient;
            enpStartDateSetting = settingRepository.Get("EnpStartDate");
        }

        public async Task<bool> IsEnpEnabledAsync(DateTime paymentDate)
        {
            var enpStartDate = await GetEnpStartDateAsync();
            return paymentDate >= enpStartDate;
        }

        public async Task<DateTime> GetEnpStartDateAsync()
        {
            if (DateTime.TryParseExact(enpStartDateSetting.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var enpStartDate))
            {
                return enpStartDate;
            }

            var context = contextAccessor.ExecutionInfoContext;

            var isEnpEnabled2022 = await firmFlagsApiClient.IsEnableAsync(context.FirmId, context.UserId, "isEnpEnabled2022");
            if (isEnpEnabled2022)
            {
                return DefaultEnpStartDate.AddYears(-1);
            }

            var isEnpEnabled = await firmFlagsApiClient.IsEnableAsync(context.FirmId, context.UserId, "isEnpEnabled");
            if (isEnpEnabled)
            {
                return DefaultEnpStartDate;
            }

            return DateTime.MaxValue;
        }
    }
}
