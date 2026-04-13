using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface IProfOutsourceApiClient
    {
        Task<ProfOutsourceContextDto> GetOutsourceContextAsync(int firmId, int userId);
    }
}