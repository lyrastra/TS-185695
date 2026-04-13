using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.NdsDeduction;
using Moedelo.Docs.ApiClient.Abstractions.NdsDeduction.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.NdsDeduction
{
    [InjectAsSingleton(typeof(IUpdsNdsDeductionApiClient))]
    public class UpdsNdsDeductionApiClient : BaseApiClient, IUpdsNdsDeductionApiClient
    {
        public UpdsNdsDeductionApiClient(
            IHttpRequestExecuter httpRequestExecuter, 
            IUriCreator uriCreator, 
            IAuditTracer auditTracer, 
            IAuthHeadersGetter authHeadersGetter, 
            IAuditHeadersGetter auditHeadersGetter, 
            ISettingRepository settingRepository,
            ILogger<UpdsNdsDeductionApiClient> logger) 
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

        public Task<List<DeductionAcceptedDto>> GetAcceptedAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            return PostAsync<IReadOnlyCollection<long>, List<DeductionAcceptedDto>>(
                "/private/api/v1/NdsDeduction/GetAccepted", 
                documentBaseIds);
        }

        public async Task<decimal> GetAvailableAsync(NdsDeductionRequestDto requestDto)
        {
            var url = $"/private/api/v1/NdsDeduction/GetAvailable";
            var response = await PostAsync<NdsDeductionRequestDto, DataResponse<decimal>>(url, requestDto);
            return response.Data;
        }

        public async Task<IReadOnlyList<DeductibleUpdDto>> GetPayableUpdsAsync(DeductibleDocumentsRequestDto requestDto)
        {
            var url = $"/private/api/v1/NdsDeduction/GetPayableUpds";
            var response = await PostAsync<DeductibleDocumentsRequestDto, DataResponse<IReadOnlyList<DeductibleUpdDto>>>(url, requestDto);
            return response.Data;
        }

        public async Task<IReadOnlyList<DeductibleUpdDto>> GetRefundableUpdsAsync(DeductibleDocumentsRequestDto requestDto)
        {
            var url = $"/private/api/v1/NdsDeduction/GetRefundableUpds";
            var response = await PostAsync<DeductibleDocumentsRequestDto, DataResponse<IReadOnlyList<DeductibleUpdDto>>>(url, requestDto);
            return response.Data;
        }
    }
}