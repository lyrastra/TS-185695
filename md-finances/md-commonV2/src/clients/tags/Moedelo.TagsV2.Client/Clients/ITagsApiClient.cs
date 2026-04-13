using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.TagsV2.Client.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.TagsV2.Client.Clients
{
    public interface ITagsApiClient : IDI
    {
        Task<TagDto> GetByNameAsync(int firmId, int userId, string name);

        Task<List<TagDto>> GetByNamesAsync(int firmId, int userId, IReadOnlyCollection<string> names);

        Task<List<TagDto>> GetByEntitiesAsync(int firmId, int userId, EntityListRequestDto dto);

        Task<long> SaveAsync(int firmId, int userId, TagDto dto);

        Task AttachTagsAsync(int firmId, int userId, AttachTagsRequestDto dto);

        Task DetachTagsAsync(int firmId, int userId, DetachTagsRequestDto dto);

        Task DetachTagsAsync(int firmId, int userId, EntityListRequestDto dto);

        Task UpdateEntityNonSystemTagsAsync(int firmId, int userId, UpdateEntityTagsRequestDto dto);

        /// <summary>
        /// Костыль для объединения контрагентов
        /// </summary>
        // todo: передалеть на обобщеный вариант
        Task<List<TagDto>> GetForUnionKontragentsAsync(int firmId, int userId, GetForUnionKontragentsRequestDto dto);
    }
}
