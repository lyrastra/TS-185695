using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.PayrollV2.Client.Employees
{
    /// <summary>
    /// Клиент для импорта выписок
    /// </summary>
    public interface IEmployeesForImportApiClient : IDI
    {
        /// <summary>
        ///  Возвращает список работающих сотрудников на указанную дату
        /// </summary>
        Task<List<NotFiredWorkerDto>> GetNotFiredWorkersAsync(int firmId, int userId, DateTime date);

        Task<WorkerForPaymentImportModelDto[]> GetAsync(int firmId, int userId);
    }
}
