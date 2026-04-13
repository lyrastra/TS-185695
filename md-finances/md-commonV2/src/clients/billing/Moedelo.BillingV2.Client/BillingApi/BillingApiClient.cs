
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.Billing;
using Moedelo.BillingV2.Dto.Billing.PaymentPositions;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.BillingApi
{
    [InjectAsSingleton]
    public class BillingApiClient : BaseApiClient, IBillingApiClient
    {
        private readonly SettingValue apiEndPoint;

        public BillingApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<RepeatPaymentResponseDto>> RepeatPaymentsAsync(List<RepeatPaymentRequestDto> paymentsDtos)
        {
            return PostAsync<List<RepeatPaymentRequestDto>, List<RepeatPaymentResponseDto>>("/V2/RepeatPayments", paymentsDtos);
        }

        public Task<List<PaymentHistoryWithExDto>> GetPaymentHistoryWithExByIdsAsync(
            IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<PaymentHistoryWithExDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<PaymentHistoryWithExDto>>("/V2/GetPaymentHistoryWithExByIds", ids);
        }

        public Task<List<PaymentHistoryWithExDto>> GetPaymentHistoryWithExByFirmIdsAsync(IEnumerable<int> firmIds)
        {
            return PostAsync<IEnumerable<int>, List<PaymentHistoryWithExDto>>("/V2/GetPaymentHistoryWithExByFirmIds", firmIds);
        }

        public Task<bool?> IsLastPaymentAsync(int paymentId, bool? success)
        {
            return GetAsync<bool?>("/V2/IsLastPayment", new { paymentId, success });
        }

        public Task<Dictionary<int, int[]>> GetPaymentGroupsAsync(
            IReadOnlyCollection<int> paymentIds)
        {
            if (paymentIds?.Any() != true)
            {
                return Task.FromResult(new Dictionary<int, int[]>());
            }

            return PostAsync<IReadOnlyCollection<int>, Dictionary<int, int[]>>("/V2/GetPaymentGroups", paymentIds);
        }

        public Task<PaymentHistoryDto> GetCurrentPaymentWithTrialAsync(int firmId)
        {
            return GetAsync<PaymentHistoryDto>("/V2/GetCurrentPaymentWithTrial", new { firmId });
        }

        public Task<TariffDto> GetCurrentTariffAsync(int firmId)
        {
            return GetAsync<TariffDto>("/V2/GetCurrentTariff", new { firmId });
        }

        public Task<int> SavePaymentHistoryAsync(PaymentHistoryDto paymentHistory)
        {
            return PostAsync<PaymentHistoryDto, int>("/V2/SavePaymentHistory", paymentHistory);
        }

        public Task<int> SavePaymentHistoryAndUpdatePositionsAsync(PaymentHistoryAndPositionsDto paymentHistoryAndPositions)
        {
            return PostAsync<PaymentHistoryAndPositionsDto, int>("/V2/SavePaymentHistoryAndUpdatePositions", paymentHistoryAndPositions);
        }

        public Task<ShortTariffInfoDto> GetShortCurrentTariffInfoAsync(int firmId)
        {
            return GetAsync<ShortTariffInfoDto>("/V2/GetShortCurrentTariffInfo", new { firmId });
        }

        [Obsolete("Используй AccountingActApiClient")]
        public Task<List<PositionByPaymentDto>> GetActsByPaymentAsync(int paymentId)
        {
            return GetAsync<List<PositionByPaymentDto>>("/V2/GetActsByPayment", new { paymentId });
        }

        public Task SwitchIsRefundStateAsync(SwitchIsRefundStateRequestDto dto)
        {
            return PostAsync("/V2/SwitchIsRefundState", dto);
        }

        public Task<List<PaymentHistoryForBillingDto>> GetPaymentHistoryForBillingByFirmIdAsync(int firmId)
        {
            return GetAsync<List<PaymentHistoryForBillingDto>>("/V2/GetPaymentHistoryForBillingByFirmId", new { firmId });
        }

        public Task UpdatePaymentPositionsAsync(UpdatePaymentPositionsDto dto)
        {
            return PostAsync($"/PaymentHistory/{dto.PaymentId}/positions", dto.Positions);
        }

        public Task<List<PaymentPositionDto>> GetPaymentHistoryPositions(int paymentId)
        {
            return GetAsync<List<PaymentPositionDto>>($"/PaymentHistory/{paymentId}/positions");
        }

        public Task<PaymentHistoryExBillDataDto> GetPaymentHistoryExBillDataAsync(int paymentId)
        {
            return GetAsync<PaymentHistoryExBillDataDto>("/V2/GetPaymentHistoryExBillData", new { paymentId });
        }
        
        public Task<List<PaymentHistoryExBillDataDto>> GetPaymentHistoryExBillDataAsync(
            IReadOnlyCollection<int> paymentIds)
        {
            return PostAsync<IReadOnlyCollection<int>, List<PaymentHistoryExBillDataDto>>(
                "/V2/GetPaymentsHistoryExBillData", paymentIds);
        }

        public Task SavePaymentHistoryExBillDataAsync(PaymentHistoryExBillDataDto dto)
        {
            return PostAsync("/V2/SavePaymentHistoryExBillData", dto);
        }

        public Task SwitchOnPaymentsAsync(List<int> paymentIds)
        {
            return PostAsync("/V2/SwitchOnPayments", paymentIds);
        }

        public Task<List<BillingDto>> GetBillingDataAsync(int firmId)
        {
            return GetAsync<List<BillingDto>>("/V2/GetBillingData", new { firmId });
        }

        public Task<List<GroupedPaymentHistoryDto>> GetRealGroupedPaymentHistoryDtosForFirm(int firmId)
        {
            return GetAsync<List<GroupedPaymentHistoryDto>>("/V2/GetRealGroupedPaymentHistoryDtosForFirm", new { firmId });
        }

        public Task<List<PaymentHistoryWithExDto>> GetSuccessPaymentsForPeriodByPaymentMethodsAsync(GetPaymentsForPeriodByPaymentMethodsRequestDto dto)
        {
            return PostAsync<GetPaymentsForPeriodByPaymentMethodsRequestDto, List<PaymentHistoryWithExDto>>("/V2/GetSuccessPaymentsForPeriodByPaymentMethods", dto);
        }

        public Task<int> AddTrialPaymentAsync(AddTrialPaymentRequestDto dto)
        {
            return PostAsync<AddTrialPaymentRequestDto, int>("/V2/AddTrialPayment", dto);
        }

        public Task<int> SavePaymentHistoryExAsync(PaymentHistoryWithExDto dto)
        {
            return PostAsync<PaymentHistoryWithExDto, int>("/V2/SavePaymentHistoryEx", dto);
        }

        public async Task<bool> IsSubscriptionExpiredAsync(int firmId)
        {
            var result = await GetAsync<DataRequestWrapper<bool>>("/IsSubscriptionExpired", new { firmId }).ConfigureAwait(false);
            return result.Data;
        }

        public Task<UserPaymentExtendedDto> GetCurrentUserPaymentAsync(int firmId)
        {
            return GetAsync<UserPaymentExtendedDto>("/GetCurrentUserPayment", new { firmId });
        }

        public Task<UserPaymentExtendedDto[]> GetExtendedInfoAboutSuccessPaymentsAsync(int firmId)
        {
            return GetAsync<UserPaymentExtendedDto[]>($"/GetExtendedInfoAboutSuccessPayments?firmId={firmId}");
        }

        public Task<PaymentHistoryDto> GetPaymentByIdAsync(int id)
        {
            return GetAsync<PaymentHistoryDto>("/GetPaymentById", new { id });
        }

        public Task<PaymentHistoryDto> GetLastTariffWithTrialAsync(int firmId)
        {
            return GetAsync<PaymentHistoryDto>("/GetLastTariffWithTrial", new { firmId });
        }

        public Task<List<PaymentHistoryDto>> GetPaymentHistoryForFirmAsync(int firmId)
        {
            return GetAsync<List<PaymentHistoryDto>>("/V2/GetPaymentHistoryForFirm", new { firmId });
        }

        public async Task<List<int>> GetTrialOrPaidFirmsAsync(IReadOnlyCollection<int> firmIds)
        {
            if (firmIds?.Any() != true)
            {
                return new List<int>();
            }

            var result = await PostAsync<IReadOnlyCollection<int>, DataRequestWrapper<List<int>>>(
                "/GetTrialOrPaidFirms",
                firmIds).ConfigureAwait(false);

            return result.Data;
        }

        public Task<SberbankPaymentsDto> GetSberbankPaymentsAsync(SberbankPaymentStatus status,
            string paymentMethod,
            int afterPaymentId,
            bool excludePaymentsWithEmptySberbankId,
            int? limit)
        {
            return GetAsync<SberbankPaymentsDto>(
                "/V2/GetSberbankPayments",
                new { status, paymentMethod, afterPaymentId, excludePaymentsWithEmptySberbankId, limit });
        }
        
        public Task<SberbankPaymentsDto> GetAllSberbankPaymentsByStatusAsync(SberbankPaymentStatus status, string paymentMethod)
        {
            return GetAsync<SberbankPaymentsDto>("/V2/GetAllSberbankPaymentsByStatus", new { status, paymentMethod });
        }

        public Task<PaymentHistoryDto> PayAsync(PayRequestDto payDto)
        {
            return PostAsync<PayRequestDto, PaymentHistoryDto>("/Pay", payDto);
        }

        public Task<TariffDto> GetTariffByPriceListIdAsync(int id)
        {
            return GetAsync<TariffDto>("/GetTariffByPriceListId", new { id });
        }

        public Task<PaymentHistoryDto> GetPaymentByTransactionId(int id)
        {
            return GetAsync<PaymentHistoryDto>("/GetPaymentByTransactionId", new { id });
        }

        public Task DeletePaymentAsync(int id)
        {
            return GetAsync("/DeletePayment", new { id });
        }

        public Task SwitchOnPaymentAsync(SwitchOnPaymentRequestDto request)
        {
            return PostAsync("/SwitchOnPayment", request);
        }

        public Task SwitchOffPaymentAsync(SwitchOffPaymentRequestDto requestDto)
        {
            return PostAsync("/SwitchOffPayment", requestDto);
        }

        public async Task<IList<TariffNameDto>> GetTariffNamesByPriceListIdsAsync(IReadOnlyList<int> ids)
        {
            var result = await PostAsync<IReadOnlyList<int>, ListRequestWrapper<TariffNameDto>>("/GetTariffNamesByPriceListIds", ids).ConfigureAwait(false);
            return result.Items;
        }

        public Task<DataRequestWrapper<bool>> ProlongCurrentPaymentAsync(int firmId, int userId, int companyId)
        {
            return PostAsync<DataRequestWrapper<int>, DataRequestWrapper<bool>>("/ProlongCurrentPayment", new DataRequestWrapper<int>{ Data = companyId });
        }

        public Task UpdateSberbankPaymentStatuses(List<UpdateSberbankPaymentStatusDto> dtos)
        {
            return PostAsync("/V2/UpdateSberbankPaymentStatuses", dtos);
        }

        public Task<PaymentHistoryDto> PayWithBillAsync(PayRequestDto payDto)
        {
          
            return PostAsync<PayRequestDto, PaymentHistoryDto>("/PayWithBill", payDto);
        }

        public Task<PaymentHistoryDto> GetFirstUnsuccessfulPaymentForGroupAsync(int anyPaymentIdInGroup, long transactionId)
        {        
            return GetAsync<PaymentHistoryDto>("/GetFirstUnsuccessfulPaymentForGroupAsync", new { anyPaymentIdInGroup, transactionId });
        }

        public Task ExpirePaymentForFirmsAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync("/ExpirePaymentForFirms", firmIds);
        }

        public Task LinkPayPartPayments(IReadOnlyCollection<int> paymentIds)
        {
            return PostAsync("/LinkPayPartPayments", paymentIds);
        }

        public Task MarkPaymentsAsTrackedAsync(int firmId, IReadOnlyCollection<int> paymentIds)
        {
            return PutAsync($"/v2/MarkPaymentsAsTracked?firmId={firmId}", paymentIds);
        }
    }
}
