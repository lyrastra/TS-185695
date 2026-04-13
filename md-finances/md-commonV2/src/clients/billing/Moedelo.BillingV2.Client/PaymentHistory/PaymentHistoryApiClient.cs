using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.Billing;
using Moedelo.BillingV2.Dto.Billing.PaymentPositions;
using Moedelo.BillingV2.Dto.PaymentHistory;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.PaymentHistory
{
    [InjectAsSingleton]
    public class PaymentHistoryApiClient : BaseApiClient, IPaymentHistoryApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PaymentHistoryApiClient(
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
        
        public Task<PaymentHistoryDto> GetAsync(int id)
        {
            return GetAsync<PaymentHistoryDto>($"/PaymentHistory/{id}");
        }
        
        public Task<List<PaymentHistoryDto>> GetAsync(IReadOnlyCollection<int> ids)
        {
            if (!ids.Any())
            {
                return Task.FromResult(new List<PaymentHistoryDto>());
            }

            return PostAsync<IEnumerable<int>, List<PaymentHistoryDto>>("/PaymentHistory/Get", ids.Distinct());
        }

        public Task<List<PaymentHistoryDto>> GetAsync(PaymentHistoryRequestDto criteria)
        {
            return PostAsync<PaymentHistoryRequestDto, List<PaymentHistoryDto>>("/PaymentHistory/GetBy", criteria);
        }

        public Task MarkAsExportedTo1cAsync(PaymentHistoryMarkAsExportedTo1cRequestDto requestDto, HttpQuerySetting setting = null)
        {
            if (requestDto?.PaymentIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync("/PaymentHistory/MarkAsExportedTo1c", requestDto, setting: setting);
        }

        public Task<List<PaymentPositionDto>> GetPositionsAsync(int paymentHistoryId)
        {
            return GetAsync<List<PaymentPositionDto>>($"/PaymentHistory/{paymentHistoryId}/positions");
        }

        public Task<Dictionary<int, List<PaymentPositionDto>>> GetPositionsAsync(IReadOnlyCollection<int> paymentHistoryIds)
        {
            return PostAsync<IReadOnlyCollection<int>, Dictionary<int, List<PaymentPositionDto>>>(
                "/PaymentHistory/GetPositions", paymentHistoryIds);
        }

        public Task UpdatePositionsAsync(int paymentHistoryId, IReadOnlyCollection<PaymentPositionDto> positionDtos)
        {
            return PostAsync($"/PaymentHistory/{paymentHistoryId}/positions", positionDtos);
        }

        public Task<IReadOnlyDictionary<int, List<PaymentHistoryDto>>> GetActiveOrLastWithAccessAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, List<PaymentHistoryDto>>>(
                $"/PaymentHistory/GetLastWithAccess", 
                firmIds
            );
        }

        public Task UpdateValidityPeriodAsync(UpdateValidityPeriodRequestDto requestDto)
        {
            return PostAsync("/PaymentHistory/UpdateValidityPeriod", requestDto);
        }

        public Task UpdatePositionsAndUpdateValidityPeriodAsync(PositionsAndValidityPeriodRequestDto requestDto)
        {
            return PostAsync("/PaymentHistory/UpdatePositionsAndUpdateValidityPeriod", requestDto);
        }

        public Task UpdateIncomingDateAsync(UpdateIncomingDateRequestDto requestDto)
        {
            return PostAsync("/PaymentHistory/UpdateIncomingDate", requestDto);
        }
    }
}