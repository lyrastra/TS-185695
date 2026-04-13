using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.IntegratedFile;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.IntegratedFile
{
    public interface IIntegratedFileClient : IDI
    {
        /// <summary> Возвращает выписки для импорта </summary>
        Task<IList<IntegratedFilesDto>> GetIntegratedFilesForFirmAsync(int firmId);

        /// <summary> Возвращает выписки для импорта </summary>
        Task<IntegratedFilesDto> GetIntegratedFileForFirmByIdAsync(int integratedFileId, int firmId);

        /// <summary> Помечает файл как пропущенный </summary>
        Task<string> MarkIntegratedFileSkippedAsync(int integratedFileId, int firmId);

        /// <summary> Помечает файл как добавленный </summary>
        Task<string> MarkIntegratedFileAddedAsync(int integratedFileId, int firmId);

        /// <summary> Сохраняет выписку для указанной фирмы и интеграции </summary>
        Task<bool> TransferFilesToMdAsync(MDMovementList movementList, int firmId, IntegrationPartners integrationPartner, string requestId = "");

        Task<List<IntegratedFileGeneralInfoDto>> GetIntegratedFilesGeneralInfoAsync(int firmId);

        Task<IntegrationResponseDto<IntWithNewFilesResponseDto>> GetActiveIntegrationsWithNewFilesAsync(int firmId);

        Task<IntegrationResponseDto<List<IntegratedFileGeneralInfoDto>>> GetIntegratedFilesGeneralInfoForPartnerAsync(
            int firmId,
            IntegrationPartners integrationPartner);

        /// <summary> Возвращает последнюю выписку </summary>
        Task<IntegrationResponseDto<IntegratedFileGeneralInfoDto>> GetLastIntegratedFileByIntegrationPartnerAsync
            (int firmId, 
            IntegrationPartners integrationPartner);
    }
}
