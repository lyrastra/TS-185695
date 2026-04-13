using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.BankOperation;

namespace Moedelo.BankIntegrationsV2.Client.Abstraction.SberbankBip
{
    public interface ISberbankBipClient : IDI
    {
        Task<bool> LandingAccessDeniedForBipAsync(string externalClientId);
        Task<SberbankBipResponseDto> MovingBipToMyDealAsync(int firmId, int userId);
    }
}
