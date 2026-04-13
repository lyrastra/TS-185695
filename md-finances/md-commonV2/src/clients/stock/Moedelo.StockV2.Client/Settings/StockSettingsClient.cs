using System;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Stocks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto.Settings;

namespace Moedelo.StockV2.Client.Settings
{
    [InjectAsSingleton(typeof(IStockSettingsClient))]
    public class StockSettingsClient : BaseApiClient, IStockSettingsClient
    {
        private readonly SettingValue apiEndPoint;

        public StockSettingsClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : 
            base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<bool> IsStockEnabledAsync(int firmId, int userId)
        {
            var response = await GetAsync<DataResponse<bool>>("/StockSettings/GetStockState", new { firmId, userId }).ConfigureAwait(false);
            return response.Data;
        }

        public Task<StockSettingsDto> GetAsync(int firmId, int userId)
        {
            return GetAsync<StockSettingsDto>("/StockSettings/GetStockSettings", new
            {
                firmId,
                userId
            });
        }

        public Task<StockSettingsDto> EnableAsync(int firmId, int userId, DateTime stockActivationDate)
        {
            var utcTime = stockActivationDate.ToUniversalTime();
            var uri = $"/StockSettings/CreateStockSettings?firmId={firmId}&userId={userId}&stockActivationDate={utcTime:yyyy-MM-ddTHH:mm:ssZ}";

            return PostAsync<StockSettingsDto>(uri);
        }

        public async Task<StockDisableResponseStatusCode> DisableAsync(int firmId, int userId)
        {
            var response = await PostAsync<StockDisableResponseDto>($"/StockSettings/SetStockDisabled?firmId={firmId}&userId={userId}").ConfigureAwait(false);
            return response.Status;
        }
    }
}