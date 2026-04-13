using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.Money
{
    public interface IOperationsAccessApiClient
    {
        Task<OperationsAccessDto> GetAccessAsync();
    }
}
