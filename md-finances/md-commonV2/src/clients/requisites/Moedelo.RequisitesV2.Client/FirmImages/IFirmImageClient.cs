using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.FirmImages;

namespace Moedelo.RequisitesV2.Client.FirmImages
{
    public interface IFirmImageClient : IDI
    {
        Task<Dictionary<FirmImageType, byte[]>> GetAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<FirmImageType> types);
        
        Task<List<FirmImageType>> GetImageTypesAsync(
            int firmId,
            int userId);
        
        Task<Dictionary<FirmImageType, FirmImageWithOffsetDto>> GetImagesWithOffsetsAsync(int firmId, int userId,
            IReadOnlyCollection<FirmImageType> types);
    }
}