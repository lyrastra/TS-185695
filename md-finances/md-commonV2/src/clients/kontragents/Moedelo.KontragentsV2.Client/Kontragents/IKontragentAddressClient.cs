using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Kontragents.Address;
using Moedelo.KontragentsV2.Dto.Address;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    public interface IKontragentAddressClient
    {
        Task<KontragentAddressDto> GetByKontragentIdAndAddressType(int firmId, int userId, int kontragentId, AddressType addressType);
    }
}
