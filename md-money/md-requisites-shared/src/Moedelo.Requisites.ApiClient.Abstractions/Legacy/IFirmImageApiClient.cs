using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legasy.Dto;
using Moedelo.Requisites.Enums.FirmRequisites;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IFirmImageApiClient
    {
        Task<Dictionary<FirmImageType, byte[]>> GetImagesAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<FirmImageType> types);

        Task<Dictionary<FirmImageType, FirmImageWithOffsetDto>> GetImagesWithOffsetsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<FirmImageType> types);
    }
}