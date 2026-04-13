using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KontragentsV2.Dto.Contacts;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    public interface IKontragentContactClient : IDI
    {
        Task<List<KontragentContactDto>> GetByKontragentsAsync(int firmId, int userId,
            IReadOnlyCollection<int> kontragentIds);

        Task<long> AddAsync(int firmId, int userId, KontragentContactDto contact);
    }
}