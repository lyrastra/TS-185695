using System.Threading.Tasks;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface IOperatorDepartmentApiClient
    {
        Task<OperatorDepartmentDto> GetByIdAsync(int id);
    }
}