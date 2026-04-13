using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Estate.ApiClient.Abstractions.legacy.FixedAsset.Dto;

namespace Moedelo.Estate.ApiClient.Abstractions.legacy.FixedAsset
{
    public interface IFixedAssetApiClient
    {
        Task<FixedAssetDto> GetByBaseId(FirmId firmId, UserId userId, long baseId);
    }
}
