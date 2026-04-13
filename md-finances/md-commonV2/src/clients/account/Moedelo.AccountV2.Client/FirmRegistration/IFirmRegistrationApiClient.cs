using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.FirmRegistration;

namespace Moedelo.AccountV2.Client.FirmRegistration
{
    public interface IFirmRegistrationApiClient
    {
        Task Save(FirmRegistrationV2Dto firmRegistrationV2Dto);
    }
}