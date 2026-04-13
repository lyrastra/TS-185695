using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Products;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountV2.Client.ProductPartition
{
    public interface IProductPartitionApiClient : IDI
    {
        /// <summary> 
        /// Возвращает product partition пользователя
        /// </summary>
        Task<WLProductPartition> GetAsync(int userId, int firmId = 0);
    }
}