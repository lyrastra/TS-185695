using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.StockV2.Client.Stocks
{
    public interface IStockInitializationClient : IDI
    {
        Task InitializeAsync(int firmId, int userId);
    }
}