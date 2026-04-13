using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.Abstractions.SyntheticAccounts;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.CashOrders;
using Moedelo.Money.Domain.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueUpdater))]
    internal class RetailRevenueUpdater : IRetailRevenueUpdater
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly RetailRevenueEventWriter retailRevenueEventWriter;
        private readonly RetailRevenueApiClient retailRevenueApiClient;
        private readonly ICashOrderApiClient cashOrderApiClient;
        private readonly ISyntheticAccountReader syntheticAccountReader;

        public RetailRevenueUpdater(
            IExecutionInfoContextAccessor executionInfoContext,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            RetailRevenueEventWriter retailRevenueEventWriter,
            RetailRevenueApiClient retailRevenueApiClient,
            ICashOrderApiClient cashOrderApiClient,
            ISyntheticAccountReader syntheticAccountReader)
        {
            this.executionInfoContext = executionInfoContext;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.retailRevenueEventWriter = retailRevenueEventWriter;
            this.retailRevenueApiClient = retailRevenueApiClient;
            this.cashOrderApiClient = cashOrderApiClient;
            this.syntheticAccountReader = syntheticAccountReader;
        }

        public async Task<CashOrderSaveResponse> UpdateAsync(RetailRevenueSaveRequest request)
        {
            await UpdateOperationAsync(request).ConfigureAwait(false);
            return new CashOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private async Task UpdateOperationAsync(RetailRevenueSaveRequest request)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            if (request.TaxationSystemType == null)
            {
                request.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year).ConfigureAwait(false);
            }
            var syntheticAccountCode = request.TaxationSystemType == TaxationSystemType.Envd
                ? SyntheticAccountCode._90_01_02
                : SyntheticAccountCode._90_01_01;
            request.SyntheticAccountTypeId = await syntheticAccountReader.GetIdByCodeAsync(syntheticAccountCode).ConfigureAwait(false);
            await retailRevenueApiClient.UpdateAsync(request).ConfigureAwait(false);
            await retailRevenueEventWriter.WriteUpdatedEventAsync(request).ConfigureAwait(false);
            await cashOrderApiClient.ProvideAsync(context.FirmId, context.UserId, new List<long> { request.DocumentBaseId } ).ConfigureAwait(false);
        }
    }
}