using System.Threading.Tasks;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IStockSettingsApiCient
    {
        Task<bool> IsStockEnabledAsync(int firmId, int userId);
    }
}