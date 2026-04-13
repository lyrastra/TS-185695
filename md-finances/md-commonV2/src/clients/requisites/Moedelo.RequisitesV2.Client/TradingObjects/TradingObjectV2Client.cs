using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.TradingObjects;

namespace Moedelo.RequisitesV2.Client.TradingObjects
{
    [InjectAsSingleton]
    public class TradingObjectV2Client : BaseApiClient, ITradingObjectV2Client
    {
        private readonly SettingValue apiEndPoint;

        public TradingObjectV2Client(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<TradingObjectV2Dto> GetByIdAsync(int firmId, int userId, int id)
        {
            var result = await GetAsync<DataWrapper<TradingObjectV2Dto>>(
                "/TradingObject/GetTradingObject",
                new { firmId, userId, id}).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<TradingObjectV2Dto>> GetByCriteriaAsync(int firmId, int userId, TradingObjectCriteriaDto criteria)
        {
            var listDto = await GetAsync<ListWrapper<TradingObjectV2Dto>>(
                "/TradingObject/GetTradingObjectListByCritery",
                new
                {
                    firmId,
                    userId,
                    criteria.RegionCode,
                    criteria.Year,
                    criteria.IsActual
                }).ConfigureAwait(false);
            return listDto.Items ?? new List<TradingObjectV2Dto>();
        }

        public async Task<List<TradingObjectV2Dto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> tradingObjectIds)
        {
            if (tradingObjectIds?.Any() != true)
            {
                return new List<TradingObjectV2Dto>();
            }
            
            var result = await PostAsync<IReadOnlyCollection<int>, DataWrapper<List<TradingObjectV2Dto>>>(
                $"/TradingObject/GetTradingObject?firmId={firmId}&userId={userId}",
                tradingObjectIds).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<List<TradingObjectV2ShortDto>> GetShortAsync(int firmId, int userId)
        {
            var listDto = await GetAsync<ListWrapper<TradingObjectV2ShortDto>>(
                "/TradingObject/GetShortTradingObjectsByFirmId",
                new { firmId, userId }).ConfigureAwait(false);
            return listDto.Items ?? new List<TradingObjectV2ShortDto>();
        }

        public async Task<List<TradingObjectV2WizardDto>> GetForWizardAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            var listDto = await GetAsync<ListWrapper<TradingObjectV2WizardDto>>(
                "/TradingObject/GetTradingObjectListForWizard",
                new
                {
                    firmId,
                    userId,
                    startDate,
                    endDate
                }).ConfigureAwait(false);
            return listDto.Items ?? new List<TradingObjectV2WizardDto>();
        }

        public async Task SetTradingObjectFnsNotifiedAsync(int firmId, int id, bool fnsNotified)
        {
            await GetAsync(
                "/TradingObject/SetTradingObjectFnsNotified",
                new
                {
                    firmId,
                    id,
                    fnsNotified
                }).ConfigureAwait(false);
        }

        public async Task SetTradingObjectHistoryFnsNotifiedAsync(int firmId, int historyId, bool fnsNotified)
        {
            await GetAsync(
                "/TradingObject/SetTradingObjectHistoryFnsNotified",
                new
                {
                    firmId,
                    historyId,
                    fnsNotified
                }).ConfigureAwait(false);
        }
        
        public async Task<TradingObjectV2EventDto> GetTradingObjectEventAsync(int firmId, int userId, int calendarId)
        {
            var dto = await GetAsync<DataWrapper<TradingObjectV2EventDto>>(
                "/TradingObject/GetTradingObjectEvent",
                new
                {
                    firmId,
                    userId,
                    calendarId
                }).ConfigureAwait(false);
            return dto.Data ?? new TradingObjectV2EventDto();
        }
        
        public async Task<Dictionary<int, TradingObjectV2EventDto>> GetTradingObjectEventsAsync(int firmId, int userId, IReadOnlyCollection<int> calendarIds)
        {
            var dto = await PostAsync<IReadOnlyCollection<int>, DataWrapper<Dictionary<int, TradingObjectV2EventDto>>>(
                $"/TradingObject/GetTradingObjectEvents?firmId={firmId}&userId={userId}",
                calendarIds).ConfigureAwait(false);
            return dto.Data ?? new Dictionary<int, TradingObjectV2EventDto>();
        }
        
        public async Task<decimal> GetTaxSumAsync(TradingTaxCalculationV2Dto calculationDto)
        {
            var dto = await PostAsync<TradingTaxCalculationV2Dto, DataWrapper<decimal>>(
                "/TradingObject/CalculateTax", calculationDto).ConfigureAwait(false);
            return dto.Data;
        }
    }
}