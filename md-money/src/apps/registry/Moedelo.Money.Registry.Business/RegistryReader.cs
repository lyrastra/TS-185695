using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Registry.Business.Abstractions;
using Moedelo.Money.Registry.DataAccess.Abstractions;
using Moedelo.Money.Registry.Domain.Models;
using Moedelo.Money.Registry.Business.Models;
using Moedelo.Money.Registry.Domain.Models.SelfCostPayments;

namespace Moedelo.Money.Registry.Business
{
    [InjectAsSingleton(typeof(IRegistryReader))]
    internal class RegistryReader : IRegistryReader
    {
        private const int MaxLimit = 5_000;

        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IBalanceMasterService balanceMasterService;
        private readonly IFirmRequisitesService firmRequisitesService;
        private readonly IRegistryDao dao;

        public RegistryReader(
            IExecutionInfoContextAccessor executionInfoContext,
            IBalanceMasterService balanceMasterService,
            IFirmRequisitesService firmRequisitesService,
            IRegistryDao dao)
        {
            this.executionInfoContext = executionInfoContext;
            this.balanceMasterService = balanceMasterService;
            this.firmRequisitesService = firmRequisitesService;
            this.dao = dao;
        }

        public async Task<OperationsResult> GetAsync(RegistryQuery query)
        {
            await PrepareQueryAsync(query);

            var context = executionInfoContext.ExecutionInfoContext;
            var data = await dao.GetAsync((int)context.FirmId, query);

            return new OperationsResult
            {
                Operations = data.Items,
                TotalCount = data.TotalCount,
                Offset = query.Offset,
                Limit = query.Limit
            };
        }

        public async Task<List<MoneyOperation>> GetOutgoingPaymentsForTaxWidgetsAsync(DateTime startDate, DateTime endDate)
        {
            var initialDate = await GetInitialDateAsync();
            if (startDate < initialDate)
            {
                startDate = initialDate;
            }

            var context = executionInfoContext.ExecutionInfoContext;
            return await dao.GetOutgoingPaymentsForTaxWidgetsAsync((int)context.FirmId, startDate, endDate);
        }

        public Task<IReadOnlyList<SelfCostPayment>> GetSelfCostPaymentsAsync(SelfCostPaymentRequest request)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            return request.Source switch
            {
                OperationSource.SettlementAccount => dao.GetBankSelfCostPaymentsAsync((int)context.FirmId, request),
                OperationSource.Cashbox => dao.GetCashSelfCostPaymentsAsync((int)context.FirmId, request),
                _ => throw new ArgumentOutOfRangeException(nameof(request.Source)),
            };
        }

        private async Task PrepareQueryAsync(RegistryQuery query)
        {
            var initialDate = await GetInitialDateAsync();

            if (query.Limit > MaxLimit || query.Limit <= 0)
            {
                query.Limit = MaxLimit;
            }

            if (query.StartDate == null || query.StartDate < initialDate)
            {
                query.StartDate = initialDate;
            }

            // EndDate should ends at 23:59:59
            query.EndDate = (query.EndDate?.Date ?? DateTime.Today)
                .AddDays(1).AddSeconds(-1);
        }

        private async Task<DateTime> GetInitialDateAsync()
        {
            var balanceMasterTask = balanceMasterService.GetStatusAsync();
            var firmRegistrationDateTask = firmRequisitesService.GetFirmRegistrationAsync();
            await Task.WhenAll(balanceMasterTask, firmRegistrationDateTask);

            return GetInitialDate(firmRegistrationDateTask.Result, balanceMasterTask.Result);
        }

        /// <summary>Дата с которой начинают учитываться операции в таблице денег</summary>
        private static DateTime GetInitialDate(DateTime? firmRegistrationDate, BalanceMasterStatus balanceMaster)
        {
            return balanceMaster.IsCompleted
                ? GetStartDateForYear(balanceMaster.Date)
                : firmRegistrationDate.Value;
        }

        private static DateTime GetStartDateForYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }
    }
}
