using System.Threading.Tasks;
using Moedelo.Changelog.Shared.Api.Abstractions.Dto;
using Moedelo.Changelog.Shared.Domain.Enums;

namespace Moedelo.Changelog.Shared.Api.Abstractions
{
    public interface IChangeLogClient
    {
        Task<ChangeLogDto> GetAsync(ChangeLogEntityType entityType, long entityId);
    }
}
