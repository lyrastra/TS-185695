using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Worker;

namespace Moedelo.PayrollV2.Client.Validation
{
    public interface IValidationClient : IDI
    {
        Task<List<WorkerRequisitesCheckDto>> ValidateWorkersForNotFilledRequisitesReportAsync(DateTime startDate, DateTime endDate, int firmId, int userId);
    }
}