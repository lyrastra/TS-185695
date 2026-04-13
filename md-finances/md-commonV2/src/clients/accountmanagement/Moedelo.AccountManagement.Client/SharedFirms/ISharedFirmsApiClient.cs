using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountManagement.Dto.SharedFirms;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountManagement.Client.SharedFirms
{
    public interface ISharedFirmsApiClient : IDI
    {
        //todo поменять порядок userId и firmId
        Task<List<FirmDto>> GetFirmsInAccountAsync(int userId, int firmId);

        Task<CreateFirmInAccountResultDto> CreateFirmInAccountAsync(
            int firmId,
            int userId,
            CreateFirmInAccountRequestDto request);
    }
}