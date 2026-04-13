using Moedelo.FileStorageV2.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.FileStorageV2.Client
{
    public interface IContractFilesClient : IDI
    {
        Task DeleteDocumentFilesAsync(int firmId, int userId, long baseId, bool ignoreError);

        Task DeleteDocumentFilesAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, bool ignoreError);

        Task UpdateJustCreatedDocumentScansPathsAsync(int firmId, int userId, long temporaryBaseId, long baseId);

        Task<FileDto> DownloadZipAsync(int firmId, int userId, long baseId);

        Task<Dto.FileDto> DownloadPdfAsync(int firmId, int userId, long baseId, string fileName);

        Task<FileDto> GetFileAsync(int firmId, int userId, long baseId, string fileName, HttpQuerySetting querySetting = null);

        Task<IEnumerable<string>> GetFilesNamesAsync(int firmId, int userId, long baseId);

        Task UploadFileAsync(int firmId, int userId, long baseId, FileDto file, HttpQuerySetting querySetting = null);
    }
}
