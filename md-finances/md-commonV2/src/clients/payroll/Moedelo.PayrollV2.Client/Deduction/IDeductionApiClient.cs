using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Deduction;

namespace Moedelo.PayrollV2.Client.Deduction
{
    public interface IDeductionApiClient: IDI
    {
        Task<List<WorkerDeductionDto>> CalculateByWorkerId(int firmId, int userId, int workerId);
        
        Task<List<WorkerDeductionDto>> CalculateForFirmByPeriod(int firmId, int userId, DateTime startDate, DateTime endDate);
    }
}