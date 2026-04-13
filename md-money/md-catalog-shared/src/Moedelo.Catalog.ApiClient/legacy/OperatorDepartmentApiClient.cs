using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Catalog.ApiClient.legacy;

[InjectAsSingleton(typeof(IOperatorDepartmentApiClient))]
public class OperatorDepartmentApiClient : BaseLegacyApiClient, IOperatorDepartmentApiClient 
{
    public OperatorDepartmentApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<OperatorDepartmentApiClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("CatalogApiEndpoint"),
            logger)
    {
    }

    public Task<OperatorDepartmentDto> GetByIdAsync(int id)
    {
        return GetAsync<OperatorDepartmentDto>("/OperatorDepartment/V2/GetById", new { id });
    }
}