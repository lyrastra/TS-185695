using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.PayrollV2.Dto.Employees;
using Moedelo.PayrollV2.Dto.Positions;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.PayrollV2.Client.Employees
{
    public interface IEmployeesApiClient
    {
        Task<WorkerDto> GetWorkerAsync(int firmId, int userId, int workerId,
            CancellationToken cancellationToken = default);

        Task<List<WorkerDto>> GetWorkersAsync(int firmId, int userId, IReadOnlyCollection<int> workerIds,
            CancellationToken cancellationToken = default);

        Task<WorkerCardAccountDto> GetWorkerCardAccount(int firmId, int userId, int workerId);

        Task<decimal> GetAverageEmployeesCount(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<decimal> GetAverageEmployeesCountForFss(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<List<WorkerAutocompleteDto>> GetAutocompleteAsync(int firmId, int userId, string query = "", int count = 5, CancellationToken cancellationToken = default);

        Task<WorkerAccountSettingAndDivisionSubcontoDto> GetWorkerAccountSettingOnDateAsync(int firmId, int userId, int workerId, DateTime date);

        Task<List<WorkerAccountSettingAndDivisionSubcontoDto>> GetWorkersAccountSettingOnDateAsync(int firmId, int userId, IEnumerable<int> workerIds, DateTime date);


        /// <summary>
        /// Получить сведения о директоре (главе) компании
        /// </summary>
        Task<WorkerDto> GetDirectorAsync(int firmId, int userId);

        /// <summary>
        /// Получить систему налогообложения, в которой мы учитываем сотрудника на указанную дату
        /// </summary>
        Task<TaxationSystemType> GetWorkerTaxationSystemOnDateAsync(int firmId, int userId, int workerId, DateTime date);

        /// <summary>
        /// Получить отдел сотрудника на указанную дату
        /// </summary>
        Task<WorkerPositionFullDto> GetWorkersDivisionByDate(int firmId, int userId, int workerId, DateTime date);

        /// <summary>
        /// Статус сотрудника-иностранца на указанную дату
        /// </summary>
        Task<WorkerForeignerStatusDto> GetWorkerForeignerStatusOnDate(int firmId, int userId, int workerId, DateTime date);

        /// <summary>
        /// Получить текущий отдел и должность сотрудника
        /// </summary>
        Task<WorkerPositionDto> GetCurrentWorkerPosition(int firmId, int userId, int workerId);

        /// <summary>
        /// Получить текущий отдел и должность сотрудников
        /// </summary>
        Task<List<WorkerPositionFullDto>> GetWorkersPositions(int firmId, int userId, IEnumerable<int> workerIds);

        /// <summary>
        ///  Возвращает список работающих сотрудников за указанный период
        /// </summary>
        Task<List<NotFiredWorkerDto>> GetNotFiredWorkersAsync(int firmId, int userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Установить субконто для сотрудников, у которых оно отсутствует
        /// </summary>
        Task ResaveWithSubcontoAsync(int userId, int firmId);

        /// <summary>
        /// Обновить поле Аванс в командировке
        /// </summary>
        Task UpdateAdvanceForBusinessTrip(int firmId, int userId, long documentBaseId, decimal advanceSum);

        /// <summary>
        /// Получить список отделов организации
        /// </summary>
        Task <IList<DivisionDto>> GetDivisionsForFirmAsync(int firmId, int userId);

        /// <summary>
        ///  Возвращает количество штатных сотрудников
        /// </summary>
        Task <int> GetStaffCountAsync(int firmId, int userId);

        Task<bool> IsPatentUsedByWorker(int firmId, int userId, long patentId, CancellationToken ct = default);

        Task<List<WorkerMonthlySalaryDto>> GetWorkersAndMonthlySalaryAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task UpdatePatentInWorkerSettingAsync(int firmId, int userId, long patentId, DateTime? start, DateTime end);

        Task<List<WorkerDto>> GetWorkersByFioAsync(int firmId, int userId, string surname, string name = null, string patronymic = null);

        Task<List<int>> GetWorkerIdsByPatentIdAsync(int firmId, int userId, long patentId);
    }
}
