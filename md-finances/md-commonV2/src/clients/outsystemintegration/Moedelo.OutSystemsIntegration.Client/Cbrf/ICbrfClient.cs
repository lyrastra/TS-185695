using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.OutSystemsIntegrationV2.Dto.ExchangeRates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.OutSystemsIntegrationV2.Client.Cbrf
{
    public interface ICbrfClient : IDI
    {
        Task<List<GetExchangeRatesResponseDto>> GetExchangeRatesAsync(GetExchangeRatesRequestDto request);
    }
}
