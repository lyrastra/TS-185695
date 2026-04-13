using Moedelo.Common.Enums.Enums.Contract;
using Moedelo.ContractsV2.Client.DtoWrappers;
using Moedelo.ContractsV2.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.ContractsV2.Client
{
    public interface IContractClient : IDI
    {
        Task<ContractDto> GetByIdAsync(int firmId, int userId, int id);

        Task<List<ContractDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids);

        Task<List<ContractDto>> GetBySubcontoIdsAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds);

        Task<List<ContractDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<List<ContractDto>> GetByNumberAsync(int firmId, int userId, string number);

        /// <summary>
        /// Возвращает "Основной договор" контрагента (создает, если его нет)
        /// </summary>
        Task<ContractDto> GetOrCreateMainContractAsync(int firmId, int userId, int kontragentId);

        /// <summary>
        /// Возвращает "Основные договоры" по списку контрагентов (НЕ создает, если договор не найден)
        /// </summary>
        Task<List<ContractDto>> GeMainContractsByKontragentIdsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);

        Task<List<ContractAutocompleteItemDto>> GetAutocompleteAsync(int firmId, int userId, string query = "",
            int? count = null, int? kontragentId = null, bool onlyFounders = false);

        Task<ContractSavedDto> SaveAync(int firmId, int userId, ContractV1Dto dto);

        Task<List<ContractV1Dto>> GetListAsync(int firmId, int userId, string query, int offset, int count,
            int? kontragentId);

        Task<List<ContractFileDto>> GetContractFilesByKontragentIdsAsync(int firmId, int userId,
            IReadOnlyCollection<int> kontragentIds);

        Task<bool> CanDeleteMainContractAsync(int firmId, int userId, int kontragentId);

        Task DeleteMainContractAsync(int firmId, int userId, long kontragentId);

        Task DeleteMainContractsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);

        Task<int> GetCountByKontragentAsync(int firmId, int userId, long kontragentId);

        Task<List<ContractsCountByKontragentDto>> GetCountByKontragentsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);

        Task ReplaceKontragentInContractsAsync(int firmId, int userId, ReplaceKontragentDto request, IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null, HttpQuerySetting setting = null);

        Task<List<ContractSearchResultDto>> Search(int firmId, int userId, ContractSearchCriteriaDto criteriaDto);

        Task<RentalContractDto> GetRentalByIdAsync(int firmId, int userId, int baseId, HttpQuerySetting setting = null);

        Task<List<RentalContractDto>> GetRentalByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<List<RentalPaymentItemDto>> GetRentalPaymentItemsByContractBaseIds(int firmId, int userId, IReadOnlyCollection<long> contractBaseIds);

        Task<bool> AnyMediationContractsAsync(int firmId, int userId, MediationType type);

        Task<IReadOnlyCollection<UserTemplateDto>> GetUserTemplatesAsync(int firmId, int userId);

        Task UploadUserTemplateAsync(int firmId, int userId, TemplateFileDto file);

        Task<TemplateFileDto> DownloadUserTemplateAsync(int firmId, int userId, int templateId);
        Task<HttpFileModel> GetTemplateFileForPrintAsync(int firmId, int userId, int contractId);
    }
}