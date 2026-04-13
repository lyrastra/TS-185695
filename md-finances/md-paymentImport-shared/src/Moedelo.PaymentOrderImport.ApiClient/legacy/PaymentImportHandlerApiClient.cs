using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.legacy;

namespace Moedelo.PaymentOrderImport.ApiClient.legacy
{

    [InjectAsSingleton(typeof(IPaymentImportHandlerApiClient))]
    class PaymentImportHandlerApiClient : BaseApiClient, IPaymentImportHandlerApiClient
    {
        public PaymentImportHandlerApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentImportHandlerApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("PaymentImportHandlerApiEndpoint"),
                logger)
        {
        }

        public Task<FilePreImportStatusDto> GetImportFileStatus(int firmId, string fileId)
        {
            var setting = new HttpQuerySetting(TimeSpan.FromMinutes(10));
            return GetAsync<FilePreImportStatusDto>("/private/api/v1/PaymentImport/ImportFileStatus", new {firmId, fileId}, setting: setting);
            
        }

        public Task<StartBPMImportResultDto> StartBpmImport(StartBPMImportDto dto, HttpQuerySetting setting = null)
        {
            return PostAsync<StartBPMImportDto, StartBPMImportResultDto>($"/private/api/v1/bpm/start", dto, setting: setting);
        }


    }
}