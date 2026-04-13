using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Uploaded;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.UploadedFiles.Dto;

namespace Moedelo.UploadedFiles.Client.UploadedFiles
{
    public interface IUploadedFilesApiClient
    {
        Task<List<UploadedFileDto>> GetByEntityTypeAndIdAsync(int firmId, EntityType entityType, long entityId,
            CancellationToken cancellationToken = default);

        Task<List<UploadedFileDto>> GetByEntityTypeAndIdListAsync(int firmId,
            GetByEntityTypeAndIdListRequestDto requestDto,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(int firmId, int id,
            CancellationToken cancellationToken = default);

        Task<int> CreateAsync(CreateUploadedFileDto fileInfo, HttpFileModel file);
        
        Task MoveToFirm(int fromFirmId, int toFirmId, HttpQuerySetting setting = null, CancellationToken cancellationToken = default);
    }
}