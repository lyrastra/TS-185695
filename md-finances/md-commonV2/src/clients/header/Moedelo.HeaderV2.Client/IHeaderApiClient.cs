using System.Threading.Tasks;
using Moedelo.HeaderV2.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HeaderV2.Client
{
    public interface IHeaderApiClient : IDI
    {
        Task<HeaderDto> GetHeadAsync(HeaderRequestDto request);
        
        Task<HeaderDto> GetAccountControlHeadAsync(AccountControlHeaderRequestDto request);
    }
}
