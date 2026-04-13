using System.Threading.Tasks;

namespace Moedelo.Requisites.ApiClient.Abstractions.Clients;

public interface IStockVisibilityApiClient
{
    Task<bool> IsVisibleAsync(int? year = null);
}