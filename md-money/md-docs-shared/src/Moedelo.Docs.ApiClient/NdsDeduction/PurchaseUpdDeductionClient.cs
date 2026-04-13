using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.NdsDeduction;
using Moedelo.Docs.ApiClient.Abstractions.NdsDeduction.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.NdsDeduction
{
    [InjectAsSingleton(typeof(IPurchaseUpdDeductionClient))]
    public class PurchaseUpdDeductionClient : BaseApiClient, IPurchaseUpdDeductionClient
    {
        public PurchaseUpdDeductionClient(
            IHttpRequestExecuter httpRequestExecuter, 
            IUriCreator uriCreator, 
            IAuditTracer auditTracer, 
            IAuthHeadersGetter authHeadersGetter, 
            IAuditHeadersGetter auditHeadersGetter, 
            ISettingRepository settingRepository,
            ILogger<PurchaseUpdDeductionClient> logger) 
            : base(
                httpRequestExecuter, 
                uriCreator, 
                auditTracer, 
                authHeadersGetter, 
                auditHeadersGetter, 
                settingRepository.Get("UpdsApiEndpoint"), 
                logger)
        {
        }
        
        public Task SaveDeductionsAsync(SaveDeductionDto saveDeductionSaveRequest)
        {
            var url = $"/private/api/v1/Purchases/SaveDeduction";
            return  PostAsync(url, saveDeductionSaveRequest);
        }
    }
}