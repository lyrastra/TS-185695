using System.Threading.Tasks;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface ICountryApiClient
    {
        Task<CountryDto[]> GetAllAsync();
    }
}