using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentClient))]
    public class UnifiedBudgetaryPaymentClient: BaseCoreApiClient, IUnifiedBudgetaryPaymentClient
    {
        private readonly ISettingRepository settingRepository;
        private const string Prefix = "/api/v1";
        private const string PathUnifiedBudgetaryPayment = "PaymentOrders/Outgoing/UnifiedBudgetaryPayment";

        public UnifiedBudgetaryPaymentClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("MoneyApiEndpoint").Value;
        }

        public async Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, UnifiedBudgetaryPaymentSaveDto dto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<UnifiedBudgetaryPaymentSaveDto, ApiDataResult<PaymentOrderSaveResponseDto>>(
                $"{Prefix}/{PathUnifiedBudgetaryPayment}", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<UnifiedBudgetaryPaymentSaveDto> GetAsync(int firmId, int userId, long documentBaseId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<UnifiedBudgetaryPaymentSaveDto>>(
                $"{Prefix}/{PathUnifiedBudgetaryPayment}/{documentBaseId}", queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<PaymentOrderSaveResponseDto> UpdateAsync(int firmId, int userId, UnifiedBudgetaryPaymentSaveDto dto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PutAsync<UnifiedBudgetaryPaymentSaveDto, ApiDataResult<PaymentOrderSaveResponseDto>>(
                $"{Prefix}/{PathUnifiedBudgetaryPayment}/{dto.DocumentBaseId}", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<IReadOnlyCollection<BudgetaryAccountResponseDto>> GetAccountCodesAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<IReadOnlyCollection<BudgetaryAccountResponseDto>>>(
                $"{Prefix}/{PathUnifiedBudgetaryPayment}/GetAccountCodes", queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<IReadOnlyCollection<BudgetaryKbkAutocompleteResponseDto>> KbkAutocompleteAsync(int firmId, int userId, BudgetaryKbkAutocompleteRequestDto requestDto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<BudgetaryKbkAutocompleteRequestDto, ApiDataResult<IReadOnlyCollection<BudgetaryKbkAutocompleteResponseDto>>>(
                $"{Prefix}/{PathUnifiedBudgetaryPayment}/KbkAutocomplete", requestDto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<string> GetDescriptionAsync(int firmId, int userId, IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> subPaymentsDto, DateTime paymentDate)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<IReadOnlyCollection<UnifiedBudgetarySubPaymentDto>, ApiDataResult<string>>(
                $"{Prefix}/{PathUnifiedBudgetaryPayment}/GetDescription?paymentDate={paymentDate:yyyy-MM-dd}", subPaymentsDto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}