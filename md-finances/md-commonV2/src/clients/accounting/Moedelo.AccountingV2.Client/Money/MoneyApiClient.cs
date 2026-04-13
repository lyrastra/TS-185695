using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legasy;
using Moedelo.AccountingV2.Dto.Money;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Newtonsoft.Json;

namespace Moedelo.AccountingV2.Client.Money
{
    [InjectAsSingleton]
    public class MoneyApiClient : BaseApiClient, IMoneyApiClient
    {
        private readonly SettingValue apiEndPoint;

        public MoneyApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<string> GetOutgoingMoneyNextNumberAsync(int firmId, int userId, int year, string settlement)
        {
            var result = await GetAsync<DataResponseWrapper<string>>("/Money/GetOutgoingMoneyNextNumber", new {firmId, userId, year, settlement}).ConfigureAwait(false);
            return result.Data;
        }
        
        public async Task<int> GetOutgoingMoneyNextNumberBySettlementIdAsync(int firmId, int userId, int settlementAccountId)
        {
            var result = await GetAsync<NumberResult>("/Money/GetOutgoingMoneyNextNumberBySettlementId", new { firmId, userId, settlementAccountId }).ConfigureAwait(false);
            return result.Data;
        }
        
        public Task DeleteOperations(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            return PostAsync($"/Money/DeleteOperations?firmId={firmId}&userId={userId}", baseIds);
        }

        public async Task<List<long>> GetTradingObjectPaymentByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids)
        {
            var result = await PostAsync<IReadOnlyCollection<long>, DataResponseWrapper<List<long>>>(
                $"/Money/GetTradingObjectPaymentByIds?firmId={firmId}&userId={userId}", ids)
                .ConfigureAwait(false);
            return result.Data;
        }

        public async Task<long> CreateTradingObjectPaymentAsync(int firmId, int userId, TradingObjectOperationDto requestDto)
        {
            var result = await PostAsync<TradingObjectOperationDto, DataResponseWrapper<long>>($"/Money/CreateTradingObjectPayment?firmId={firmId}&userId={userId}", requestDto)
                .ConfigureAwait(false);
            return result.Data;
        }

        public async Task<PatentPaymentOrderDto> GetPatentPaymentOrderAsync(int firmId, int userId, long id)
        {
            return await GetAsync<PatentPaymentOrderDto>("/Money/GetPatentPaymentOrder", new { firmId, userId, id }).ConfigureAwait(false);
        }

        public async Task<List<IncomingOutgoingSumDto>> GetSumForIncomingAndOutgoingOperationsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            var result = await PostAsync<IReadOnlyCollection<int>, ListResponseWrapper<IncomingOutgoingSumDto>>(
                $"/Money/GetSumForIncomingAndOutgoingOperations?firmId={firmId}&userId={userId}", kontragentIds)
                .ConfigureAwait(false);
            return result.Items;
        }

        public Task ReplaceKontragentInMoneyAndCashAsync(int firmId, int userId, KontragentForMoneyReplaceDto request)
        {
            return PostAsync($"/Money/ReplaceKontragentInMoneyAndCash?firmId={firmId}&userId={userId}", request);
        }

        public async Task<bool> HasPurseOperationsAsync(int firmId, int userId, int purseId)
        {
            var result = await GetAsync<DataResponseWrapper<bool>>("/Money/HasPurseOperations", new { firmId, userId, purseId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<int> ProvideFinancialOperationAsync(int firmId, int userId, FinancialOperationDto dto, FinancialOperationSource source)
        {
            var uri = $"/Money/ProvideFinancialOperation?firmId={firmId}&userId={userId}&source={source}";
            var serializedString = JsonConvert.SerializeObject(dto, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            var result = await PostAsync<object, DataResponseWrapper<int>>(uri, new { dto = serializedString }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<FinancialOperationDto[]> GetFinancialOperationsAsync(int firmId, int userId, IReadOnlyCollection<int> ids)
        {
            var result = await PostAsync<IReadOnlyCollection<int>, DataResponseWrapper<string>>($"/Money/GetFinancialOperations?firmId={firmId}&userId={userId}", ids).ConfigureAwait(false);
            var dto = JsonConvert.DeserializeObject<FinancialOperationListDto>(result.Data, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            return dto.Items.ToArray();
        }

        public Task DeleteOperationsByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids, FinancialOperationSource source)
        {
            return PostAsync<object>($"/Money/DeleteOperationsByIds?firmId={firmId}&userId={userId}&source={source}", new { ids });
        }

        public async Task<int> UpdateDateInOperationAsync(int firmId, int userId, int operationId, DateTime date, FinancialOperationSource source)
        {
            var uri = $"/Money/UpdateDateInOperation?firmId={firmId}&userId={userId}&operationId={operationId}&date={date:dd.MM.yyyy}&source={source}";
            var result = await PostAsync<DataResponseWrapper<int>>(uri).ConfigureAwait(false);
            return result.Data;
        }

        public Task SaveTradingObjectPaymentLinkAsync(int firmId, int userId, TradingObjectPaymentLinkDto link)
        {
            var uri = $"/Money/SaveTradingObjectPaymentLink?firmId={firmId}&userId={userId}";
            return PostAsync(uri, link);
        }

        public Task SavePatentPaymentOrderAsync(int firmId, int userId, PatentPaymentOrderDto dto)
        {
            var uri = $"/Money/SavePatentPaymentOrder?firmId={firmId}&userId={userId}";
            return PostAsync(uri, dto);
        }

        public async Task<MoneyTransferDto[]> GetDuplicateIncomingYandexTransfersAsync(int firmId, int userId, double sum, DateTime date, int kontragentId, string description)
        {
            var uri = $"/Money/GetDuplicateIncomingYandexTransfers?firmId={firmId}&userId={userId}";
            var result = await PostAsync<object, DataResponseWrapper<MoneyTransferDto[]>>(uri, new { sum, date, kontragentId, description }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<MoneyTransferDto[]> GetDuplicateOutgoingYandexTransfersAsync(int firmId, int userId, double sum, DateTime date, int kontragentId, string description)
        {
            var uri = $"/Money/GetDuplicateOutgoingYandexTransfers?firmId={firmId}&userId={userId}";
            var result = await PostAsync<object, DataResponseWrapper<MoneyTransferDto[]>>(uri, new { sum, date, kontragentId, description }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<MoneyTransferDto[]> GetDuplicateMovementYandexTransfersAsync(int firmId, int userId, double sum, DateTime date, string documentNumber)
        {
            var uri = $"/Money/GetDuplicateMovementYandexTransfers?firmId={firmId}&userId={userId}";
            var result = await PostAsync<object, DataResponseWrapper<MoneyTransferDto[]>>(uri, new { sum, date, documentNumber }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<MoneyTransferDto[]> GetDuplicateIncomingForTypeTransferAsync(int firmId, int userId, DateTime date, double summ, string operationType, int? settlementAccountId, int? kontragentId)
        {
            var uri = $"/Money/GetDuplicateIncomingForTypeTransfer?firmId={firmId}&userId={userId}";
            var result = await PostAsync<object, DataResponseWrapper<MoneyTransferDto[]>>(uri, new { date, summ, operationType, settlementAccountId, kontragentId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<MoneyTransferDto[]> GetDuplicateIncomingTransferAsync(int firmId, int userId, DateTime date, double summ, string paymentNumber, int? settlementAccountId, int? kontragentId)
        {
            var uri = $"/Money/GetDuplicateIncomingTransfer?firmId={firmId}&userId={userId}";
            var result = await PostAsync<object, DataResponseWrapper<MoneyTransferDto[]>>(uri, new { date, summ, paymentNumber, settlementAccountId, kontragentId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<int>> GetUsingSettlementAccountIdsForKudirAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return (await GetAsync<DataResponseWrapper<List<int>>>("/Money/GetUsingSettlementAccountIdsForKudir", new { firmId, userId, startDate, endDate }).ConfigureAwait(false)).Data;
        }

        public async Task<long?> GetPatentIdByOperationAsync(int firmId, int userId, long operationBaseId)
        {
            return (await GetAsync<DataResponseWrapper<long?>>("/Money/GetPatentIdByOperation", new { firmId, userId, operationBaseId }).ConfigureAwait(false)).Data;
        }

        public async Task<MoneyTransferOperationNds> GetMoneyTransferOperationNdsAsync(int firmId, int userId, long moneyBaseId)
        {
            var result = await GetAsync<DataResponseWrapper<MoneyTransferOperationNds>>("/Money/GetMoneyTransferOperationNds", new { firmId, userId, moneyBaseId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<double> GetIncomingTransferUsnSumAsync(int firmId, DateTime startDate, DateTime endDate)
        {
            var result = await GetAsync<DataResponseWrapper<double>>("/Money/GetIncomingTransferUsnSum", new { firmId, startDate, endDate }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<double> GetIncomingTransferUsnSumByProvisionOfServicesAsync(int firmId, DateTime startDate, DateTime endDate)
        {
            var result = await GetAsync<DataResponseWrapper<double>>("/Money/GetIncomingTransferUsnSumByProvisionOfServices", new { firmId, startDate, endDate }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<double> GetOutgoingTransferUsnSumByRefundToCustomerOutgoingOperationAsync(int firmId, DateTime startDate,
            DateTime endDate)
        {
            var result = await GetAsync<DataResponseWrapper<double>>("/Money/GetOutgoingTransferUsnSumByRefundToCustomerOutgoingOperation", new { firmId, startDate, endDate }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<FundsBalanceSumDto>> GetBudgetaryOperationsForMoneyBalanceMasterFundsBalanceAsync(int firmId)
        {
            var result = await GetAsync<ListResponseWrapper<FundsBalanceSumDto>>("/Money/GetBudgetaryOperationsForMoneyBalanceMasterFundsBalance", new { firmId }).ConfigureAwait(false);
            return result.Items;
        }
    }
}
