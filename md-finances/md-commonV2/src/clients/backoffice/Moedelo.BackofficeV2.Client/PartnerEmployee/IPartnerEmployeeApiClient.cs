using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.PartnerEmployee;

namespace Moedelo.BackofficeV2.Client.PartnerEmployee
{
    public interface IPartnerEmployeeApiClient
    {
        Task<PartnerEmployeeDto> GetByUserIdAsync(int userId);
    }
}
