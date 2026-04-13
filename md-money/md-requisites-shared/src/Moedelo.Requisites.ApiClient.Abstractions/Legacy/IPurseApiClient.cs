using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IPurseApiClient
    {
        Task<PurseDto[]> GetAsync(FirmId firmId, UserId userId);

        Task<PurseDto> GetByNameAsync(FirmId firmId, UserId userId, string name);

        Task<int> SaveVirtualPurseAsync(FirmId firmId, UserId userId, PurseDto purse);
    }
}