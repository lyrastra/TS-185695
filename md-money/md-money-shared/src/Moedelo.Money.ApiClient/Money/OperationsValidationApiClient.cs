using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.Money;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;

namespace Moedelo.Money.ApiClient.Money
{
    [InjectAsSingleton(typeof(IOperationsValidationApiClient))]
    internal sealed class OperationsValidationApiClient: BaseApiClient, IOperationsValidationApiClient
    {
        public OperationsValidationApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<OperationsValidationApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyApiEndpoint"),
                logger)
        {
        }
        
        public async Task<IReadOnlyList<DocumentValidationStatusDto>> CheckByBaseIdsAsync(
            DocumentsStatusQueryDto requestDto, 
            HttpQuerySetting setting = null)
        {
            var response = await PostAsync<DocumentsStatusQueryDto, ApiPageDto<DocumentValidationStatusDto>>(
                "/private/api/v1/OperationsValidation/GetDocumentsStatusByQuery", 
                requestDto, 
                setting: setting);
            
            return response.data.ToList();
        }
    }
}