using Moedelo.Categories.Dto.CategoryType;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Categories.Client.CategoryType
{
    public interface ICategoryTypeApiClient : IDI
    {
        Task<CategoryTypeDto> InsertAsync(string name, int accountId);
    }
}