using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.Flc;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.Flc
{
    [InjectAsSingleton(typeof(IFlcApiClient))]
    public class FlcApiClient : BaseApiClient, IFlcApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public FlcApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        [Obsolete("Use IFlcNetCoreApiClient::CheckFilesAsync instead")]
        public Task<List<FlcProtocolDto>> VerifyFormatAsync(FlcDataDto flcDataDto, int firmId = 0)
        {
            return PostAsync<FlcDataDto, List<FlcProtocolDto>>($"/Flc/VerifyFormat?firmId={firmId}", flcDataDto);
        }

        [Obsolete("Use IFlcNetCoreApiClient::CheckFilesAsync instead")]
        public Task<Dictionary<string, List<string>>> VerifyAsync(FlcDataDto flcDataDto, int firmId = 0)
        {
            return PostAsync<FlcDataDto, Dictionary<string, List<string>>>($"/Flc/Verify?firmId={firmId}", flcDataDto);
        }

        public Task<FlcResultDto> VerifyContentAsync(FileDataDto fileDataDto, int firmId, int userId, bool isManualSending = false)
        {
            var uri = $"/Flc/VerifyContent?firmId={firmId}&userId={userId}&isManualSending={isManualSending}";

            return PostAsync<FileDataDto, FlcResultDto>(uri, fileDataDto);
        }
    }
}