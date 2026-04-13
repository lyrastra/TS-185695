using System.Threading.Tasks;
using Moedelo.Address.ApiClient.Abstractions.legacy.Dto.FirmAddress;

namespace Moedelo.Address.ApiClient.Abstractions.legacy
{
    public interface IFirmAddressApiClient
    {
        Task<AddressResponseModel> GetAsync(int firmId);
    }
}