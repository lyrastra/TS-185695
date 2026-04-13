using System.Threading.Tasks;
using Moedelo.Address.Dto.FirmAddress;

namespace Moedelo.Address.Client.FirmAddress
{
    public interface IFirmAddressApiClient
    {
        Task SaveAsync(int firmId, AddressRequestModel dto);
        Task<AddressResponseModel> GetAsync(int firmId);
    }
}
