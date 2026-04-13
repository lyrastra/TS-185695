using Moedelo.FirmActivityOffers.Client.Dto;
using System.Threading.Tasks;

namespace Moedelo.FirmActivityOffers.Client
{
    public interface IActivityCollectorApiClient
    {
        Task AddAsync(int firmId, int userId, AddActivityRequestDto request);
    }
}