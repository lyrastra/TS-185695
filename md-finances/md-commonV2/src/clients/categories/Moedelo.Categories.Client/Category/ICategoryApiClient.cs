using Moedelo.Categories.Dto.Category;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Categories.Client.Category
{
    public interface ICategoryApiClient : IDI
    {
        Task<CategoryDto> InsertAsync(CategoryPostDto dto);
    }
}