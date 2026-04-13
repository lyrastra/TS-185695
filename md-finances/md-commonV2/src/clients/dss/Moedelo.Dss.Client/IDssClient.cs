using System;
using System.Collections.Generic;
using Moedelo.Dss.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Dss.Client
{
    public interface IDssClient : IDI
    {
        Task<UploadCertRequestResult> UploadCertificateRequestAsync(CertificateRequestDto dto);
        Task<CertificateRequestResultDto> GetCertificateRequestResultAsync(string packetGuid);
        Task<MobileAuthDto> GetMyDssInfoAsync(string abnGuid);
        Task<string> GetCertificatePrintingFormAsync(string abnGuid);
        Task UploadSignedCertificateAsync(string abnGuid, byte[] content);
        Task<string> GetRequestAttachments(string packetId);
        Task<string> GetAttachedCertificate(string packetId);
        Task<Guid[]> CheckAttachmentsExistenceAsync(Guid[] existedPacketIds);
        Task<long> GetStekUserIdAsync(string abnGuid);
        Task<Thumbprint> GetThumbprintAsync(string abnGuid);
        Task<IReadOnlyList<CertificateInfoDto>> GetUserCertificatesAsync(string abnGuid);
        Task<ActualCertificateDto> GetActualCertificateAsync(string abnGuid);
    }
}
