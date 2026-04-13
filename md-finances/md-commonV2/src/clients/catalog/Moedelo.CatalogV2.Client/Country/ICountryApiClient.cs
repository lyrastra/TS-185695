using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Country;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.Country
{
    public interface ICountryApiClient : IDI
    {
        Task<List<CountryDto>> GetAllAsync();

        Task<string> GetAlpha3ByIsoAsync(string iso);

        Task<string> GetIsoByAlpha3Async(string alpha3);
        
        Task<string> GetNameByAlpha3Async(string alpha3);
    }
}