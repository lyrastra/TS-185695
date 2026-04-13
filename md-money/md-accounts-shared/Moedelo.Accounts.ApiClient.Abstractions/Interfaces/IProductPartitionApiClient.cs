using System.Threading.Tasks;
using Moedelo.Accounts.ApiClient.Enums;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IProductPartitionApiClient
    {
        /// <summary> 
        /// Возвращает product partition пользователя
        /// </summary>
        Task<WLProductPartition> GetAsync(int userId, int firmId = 0);
    }
}