using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.ProductMerge;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.ProductMerge
{
    public interface IProductMergeLoggingClient : IDI
    {
        Task LogAsync(int firmId, int userId, int mergeId, LogsDto logs);
    }
}