using Moedelo.Documents.Dto.Categories;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Documents.Client.Categories
{
    public interface ICategoryApiClient : IDI
    {
        Task<CategoryDto> CreateAsync(CategoryPostDto dto);
        Task<CategoryDto> GetAsync(int accountId, int id);
    }
}