using System.Threading;
using Moedelo.Common.Enums.Enums.ProductAccounting;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.ProductAccounting
{
    [InjectAsSingleton(typeof(IProductAccountingClient))]
    public class ProductAccountingClient : BaseApiClient, IProductAccountingClient
    {
        private readonly SettingValue apiEndPoint;

        public ProductAccountingClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        /// <summary>
        /// Возвращает уровень доступа к функционалу расширенного товароучёта
        /// </summary>
        public Task<ProductAccountingAccessLevel> GetAccessLevelAsync(int firmId, int userId, CancellationToken cancellationToken)
        {
            var uri = $"/ProductAccounting/GetAccessLevel?firmId={firmId}&userId={userId}";
 
            return GetAsync<ProductAccountingAccessLevel>(uri, cancellationToken: cancellationToken);
        }
    }
}