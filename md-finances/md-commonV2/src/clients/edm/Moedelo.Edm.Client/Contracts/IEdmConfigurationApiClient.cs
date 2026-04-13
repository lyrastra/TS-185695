using Moedelo.Edm.Dto.Configuration;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Edm.Client.Contracts
{
    public interface IEdmConfigurationApiClient : IDI
    {
        /// <summary>
        /// Returns true if document uploaded.
        /// </summary>
        Task<bool> IsUploadedAsync(int firmId, int entityType);

        /// <summary>
        /// Get document or null if no document uploaded.
        /// </summary>
        Task<EdmDocumentFileDto> GetAsync(int firmId, int entityType);

        /// <summary>
        /// Get document preview or null if no document uploaded.
        /// </summary>
        Task<EdmDocumentPreviewFileDto> GetPreviewAsync(int firmId, int entityType);
    }
}
