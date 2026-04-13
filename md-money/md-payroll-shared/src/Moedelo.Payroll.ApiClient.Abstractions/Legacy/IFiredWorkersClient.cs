using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FiredWorkers;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IFiredWorkersClient
    {
        Task UpdateOrderAsync(int firmId, int userId, FiredWorkerOrderUpdatingDto dto);
    }
}