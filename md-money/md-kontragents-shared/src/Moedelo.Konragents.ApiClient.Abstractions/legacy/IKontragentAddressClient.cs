using Moedelo.Konragents.Enums.Adress;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos.Address;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy
{
    public interface IKontragentAddressClient
    {
        Task<KontragentAddressDto> GetAsync(FirmId firmId, UserId userId, int kontragentId, AddressType addressType);
    }
}
