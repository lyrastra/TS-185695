using System.Threading.Tasks;
using Moedelo.SpsV2.Dto.Advertisements;

namespace Moedelo.SpsV2.Client.Advertisements
{
    public interface IAdvertisementsApiClient
    {
        Task<AdvertisementDto> GetActiveAdvertisementAsync();
    }
}