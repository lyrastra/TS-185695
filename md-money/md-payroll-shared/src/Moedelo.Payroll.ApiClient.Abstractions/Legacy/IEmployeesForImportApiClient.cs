using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IEmployeesForImportApiClient
    {
        Task<WorkerForPaymentImportDto[]> GetAsync(FirmId firmId, UserId userId);
    }
}