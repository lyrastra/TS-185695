using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Dss.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Dss.Client
{
    [InjectAsSingleton]
    public class DssClient : BaseApiClient, IDssClient
    {
        private readonly SettingValue apiEndpoint;

        public DssClient(ISettingRepository settingRepository, IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer,
            auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DssApiUrl");
        }

        public Task<UploadCertRequestResult> UploadCertificateRequestAsync(CertificateRequestDto dto)
        {
            return SendFileAsync<object, UploadCertRequestResult>("/CertificateRequest",
                new {dto.IsTestMode},
                new HttpFileModel()
                {
                    FileName = dto.FileName,
                    Stream = new MemoryStream(dto.Content),
                    ContentType = "application/octet-stream"
                });
        }

        public Task<CertificateRequestResultDto> GetCertificateRequestResultAsync(string packetId)
        {
            return GetAsync<CertificateRequestResultDto>("/CertificateRequest", new {packetId});
        }

        public Task<MobileAuthDto> GetMyDssInfoAsync(string abnGuid)
            => GetAsync<MobileAuthDto>($"/MobileAuth?login={abnGuid}");


        public Task<string> GetRequestAttachments(string packetId)
            => GetAsync<string>($"/Attachments/Request?packetId={packetId}");


        public Task<string> GetAttachedCertificate(string packetId)
            => GetAsync<string>($"/Attachments/Certificate?packetId={packetId}");


        public Task<Guid[]> CheckAttachmentsExistenceAsync(Guid[] existedPacketIds)
            => PostAsync<Guid[], Guid[]>($"/Attachments/CheckExistence", existedPacketIds);


        public Task<string> GetCertificatePrintingFormAsync(string abnGuid) 
            => GetAsync<string>($"/CertificatePrintingForm/{abnGuid}/download");


        public Task<long> GetStekUserIdAsync(string abnGuid)
            => GetAsync<long>($"/Stek/UserId?abnGuid={abnGuid}");

        public async Task<Thumbprint> GetThumbprintAsync(string abnGuid)
        {
            var res = await GetAsync<Thumbprint>($"/Users/{abnGuid}/GetThumbprint");
            return res;
        }

        public Task UploadSignedCertificateAsync(string abnGuid, byte[] content)
        {
            var request = new {FileName = $"Certificate+{abnGuid}.jpeg", Content = content};
            return PostAsync($"/CertificatePrintingForm/{abnGuid}/upload", request);
        }

        public Task<IReadOnlyList<CertificateInfoDto>> GetUserCertificatesAsync(string abnGuid)
        {
            return GetAsync<IReadOnlyList<CertificateInfoDto>>($"/certificates/{abnGuid}");
        }

        public Task<ActualCertificateDto> GetActualCertificateAsync(string abnGuid) => 
            GetAsync<ActualCertificateDto>($"/Users/{abnGuid}/actualCertificate");

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}/api/v1";
        }
    }
}
