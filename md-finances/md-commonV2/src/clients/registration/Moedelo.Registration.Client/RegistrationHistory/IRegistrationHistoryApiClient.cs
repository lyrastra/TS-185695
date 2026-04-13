using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Registration.Dto.RegistrationHistory;

namespace Moedelo.Registration.Client.RegistrationHistory
{
    public interface IRegistrationHistoryApiClient : IDI
    {
        Task AddAsync(RegistrationHistoryDto registrationHistoryRequest);

        Task UpdateUtmAsync(UtmDto utm);

        Task<List<RegistrationHistoryDto>> GetRegistrationHistoryByFirmIdAsync(int firmId);
        
        Task<FirmRegistrationHistoryDto[]> GetRegistrationHistoryByFirmIdsAsync(IReadOnlyCollection<int> firmIds);

        Task<FirmRegistrationHistoryDto[]> GetRegistrationHistoryByIdsAsync(IReadOnlyCollection<int> regIds);
    }
}