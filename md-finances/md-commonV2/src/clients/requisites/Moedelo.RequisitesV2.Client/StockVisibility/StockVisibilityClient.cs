using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto;
using Moedelo.RequisitesV2.Dto.Stock;

namespace Moedelo.RequisitesV2.Client.Stock
{
    [InjectAsSingleton(typeof(IStockVisibilityClient))]
    public class StockVisibilityClient : BaseCoreApiClient, IStockVisibilityClient
    {
        private readonly SettingValue apiEndPoint;
        private const string ControllerName = "/api/v1/StockVisibility";

        public StockVisibilityClient(
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
            apiEndPoint = settingRepository.Get("RequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public async Task<bool> IsVisibleAsync(int firmId, int userId, int? year = null,
            CancellationToken cancellationToken = default)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, cancellationToken)
                .ConfigureAwait(false);
            const string uri = "";
            var queryParams = new { year };
            var result = await GetAsync<ApiDataResult<bool>>(uri, queryParams, queryHeaders: tokenHeaders,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            return result.Data;
        }

        /// <summary>
        /// Включение отображения склада
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="switchOnWithOutValidation"></param>
        /// <returns></returns>
        public async Task<StockVisibilitySwitchResponseDto> SwitchOn(int firmId, int userId,
            bool switchOnWithOutValidation)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PutAsync<object, StockVisibilitySwitchResponseDto>(
                    $"/On?switchOnWithOutValidation={switchOnWithOutValidation}", null, queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        ///  Выключение отображения склада
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="switchOnWithOutValidation"></param>
        /// <returns></returns>
        public async Task<StockVisibilitySwitchResponseDto> SwitchOff(int firmId, int userId,
            bool switchOnWithOutValidation)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PutAsync<object, StockVisibilitySwitchResponseDto>(
                    $"/Off?switchOnWithOutValidation={switchOnWithOutValidation}", null, queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            return result;
        }
    }
}
