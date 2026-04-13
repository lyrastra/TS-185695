using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy;

public interface ITaxAdministrationApiClient
{
    Task<TaxAdministrationDto> GetByCodeAsync(int firmId, int userId, string code);
}