using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.Account;
using Moedelo.AccountV2.Dto.EntityMapping;
using Moedelo.AccountV2.Dto.Firm;
using Moedelo.Common.Enums.Enums.EntityTypes;
using Moedelo.Common.Enums.Enums.Leads;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccountV2.Client.Firm
{
    public interface IFirmApiClient : IDI
    {
        Task<FirmDto> GetAsync(int firmId, CancellationToken cancellationToken);

        Task<int> GetLegalUserId(int firmId);

        Task<int> GetLegalUserIdAsync(int firmId, CancellationToken cancellationToken);

        Task<int> CreateAsync(FirmDto firm);

        Task<int> CreateAsync(int userId, int firmId, FirmDto firm);

        Task<bool> IsSourceFirmAsync(int firmId);

        Task<int?> GetSourceFirmIdAsync(int firmId);

        Task<int?> GetTargetFirmIdAsync(int firmId);

        Task<EntityMappingDto> GetBySourceOrTargetAsync(long firmId, CancellationToken cancellationToken = default);

        Task<EntityMappingDto> GetEntityMappingBySourceIdAsync(int sourceFirmId, EntityType entityType, long sourceEntityId);

        Task<List<FirmDto>> GetFirmsAsync(IReadOnlyCollection<int> firmIds);

        Task<List<FirmIdLegalUserIdDto>> GetByLegalUsersAsync(IReadOnlyCollection<int> userIds);

        Task<int?> GetByLegalUserAsync(int userId, CancellationToken cancellationToken = default);

        Task<List<SetOfficeOperatorIdRequestDto>> GetOfficeOperatorsIdAsync(IReadOnlyCollection<int> firmIds);

        Task SetOfficeOperatorIdAsync(IReadOnlyCollection<SetOfficeOperatorIdRequestDto> requests);

        Task<LeadMarkType> GetFirmLeadMarkTypeAsync(int firmId, CancellationToken cancellationToken);

        Task<List<FirmLeadMarkDto>> GetFirmLeadMarksAsync(IReadOnlyCollection<int> firmIds);

        Task SetFirmLeadMarksAsync(FirmLeadMarkDto leadMarkDto, int logUserId, int logFirmId);

        Task SetFirmLeadMarksAsync(SetLeadMarksDto leadMarksDto, int logUserId, int logFirmId, HttpQuerySetting setting = null);

        Task<List<CompanyWithApiKeyDto>> GetCompaniesWithApiKeyAsync(int userId);

        Task ResetFirmLeadMarkByOperatorIdAsync(int operatorId);

        Task ResetFirmLeadMarkByRegionalPartnerUserIdAsync(int regionalPartnerUserId, CancellationToken cancellationToken = default);

        Task<bool> GetEmployerModeAsync(int firmId);

        Task<FirmRegistrationType> GetFirmTypeByFirmIdAsync(int firmId);

        Task<int?> GetReferralIdByFirmIdAsync(int firmId);
        
        Task<bool> CheckUserFirmIdAsync(int userId, int firmId);

        /// <summary>
        /// Проверить, имеет ли доступ указанный пользователь к указанной фирме
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>имеет ли пользователь доступ к этой фирме</returns>
        Task<bool> CheckUserHasAccessToFirmAsync(int userId, int firmId, CancellationToken cancellationToken);
        
        Task SetIsInternalAsync(int firmId, bool isInternal);

        Task<bool> GetIsInternalAsync(int firmId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Отфильтровать фирмы, не учитываемые в статистике
        /// </summary>
        /// <param name="firmIds">список идентификатор фирм</param>
        /// <returns>подсписок входного списка firmIds, содержащий только идентификаторы фирм, учитываемых в статистике (для которых is_internal = 0)</returns>
        Task<int[]> FilterOutInternalAsync(IReadOnlyCollection<int> firmIds);

        Task<bool> IsDeletedAsync(int firmId);

        Task<IReadOnlyDictionary<int, bool>> GetFlagsIsDeletedForFirmIdsAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Выставить флаг is_deleted для указанной фирмы
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task MarkAsDeletedAsync(int firmId, CancellationToken cancellationToken);
    }
}
