using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Balance;

namespace Moedelo.PayrollV2.Client.Balance
{
    public interface IWorkerBalancesApiClient : IDI
    {
        Task<IEnumerable<WorkerBalancesDto>> GetAsync(int userId, int firmId, DateTime date);
    }
}
