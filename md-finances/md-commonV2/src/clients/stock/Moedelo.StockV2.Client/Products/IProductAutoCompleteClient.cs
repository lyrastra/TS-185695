using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    public interface IProductAutoCompleteClient : IDI
    {
        Task<List<ProductAutoCompleteDto>> GetAsync(int firmId, int userId, ProductAutoCompleteRequestDto request);
    }
}