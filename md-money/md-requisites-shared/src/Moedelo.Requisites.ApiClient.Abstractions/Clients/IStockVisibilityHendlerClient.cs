using System.Threading.Tasks;

namespace Moedelo.Requisites.ApiClient.Abstractions.Clients
{
    public interface IStockVisibilityHendlerClient
    {
        Task<int[]> GetAllFirmsWithInvisibleStockAsync();
    }
}