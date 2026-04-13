using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Audit;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FssRegistries;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges;
using Moedelo.Payroll.Shared.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IWorkerApiClient
    {
        Task<IReadOnlyCollection<AuditWorkerDto>> GetWorkersByPeriod(int firmId, DateTime startDate, DateTime endDate);

        Task<List<FssRegistryWorkerInfoDto>> GetFssRegistryWorkersAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds);

        Task<List<EmploymentChangesWorkerDto>> GetEmploymentChangesWorkersAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds);

        Task<IReadOnlyCollection<NotFiredWorkerAutocompleteResponseDto>> GetNotFiredWorkersAutocomplete(FirmId firmId, UserId userId, NotFiredWorkerAutocompleteRequestDto request);

        Task<FinControlWorkerDto[]> GetForFinControlAsync(FirmId firmId, UserId userId);

        Task<WorkersSummaryForOutsourceDto> GetWorkersSummaryForOutsourceAsync(FirmId firmId, UserId userId,
            DateTime startDate, DateTime endDate);

        Task<int> CreateAsync(FirmId firmId, UserId userId, CreateWorkerDto model);

        Task<IReadOnlyCollection<WorkerForInsuredFssAutocompleteDto>> GetWorkersForInsuredFssAutocompleteAsync(
            FirmId firmId, UserId userId, WorkerForInsuredFssAutocompleteRequestDto model);

        Task<WorkerForInsuredFssDto> GetWorkerForInsuredFssAsync(FirmId firmId, UserId userId, int workerId);

        Task<int> GetNotFiredWorkerCountAsync(FirmId firmId, UserId userId, DateTime date);
        
        Task UpdateWorkingConditionsAsync(FirmId firmId, UserId userId, UpdateWorkingConditionsDto dto);

        Task<CommonWorkerDataDto> GetBySnilsAsync(FirmId firmId, string snils);
        
        Task<CommonWorkerDataDto> GetBySubcontoAsync(FirmId firmId, long subcontoId);

        Task<IReadOnlyCollection<CommonWorkerDataDto>> GetBySnilsAsync(FirmId firmId,
            IReadOnlyCollection<string> workersSnils);

        Task<List<EtrustWorkerAutocompleteDto>> GetEtrustWorkerAutocompleteAsync(int firmId);

        Task<GetWorkerDto> GetWorkerAsync(FirmId firmId, UserId userId, int workerId);

        Task<List<GetWorkerDto>> GetWorkersByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds);

        Task<GetWorkerDto[]> GetListByFioAsync(FirmId firmId, UserId userId, string surname, string name, string patronymic);

        Task<GetWorkerDto[]> GetListByPassportAsync(FirmId firmId, UserId userId, string passportSerialAndNumber);

        Task<WorkerDataForRequisitesDto> GetDataAsync(FirmId firmId, UserId userId, int workerId, DateTime? date = null, 
            CancellationToken token = default);

        Task ChangeStartDatePaymentSettingsAsync(FirmId firmId, UserId userId,
            ChangeStartDatePaymentSettingsDto request, CancellationToken token = default);

        Task UpdateWorkerYearlyIncomeAsync(FirmId firmId, UserId userId, UpdateWorkerYearlyIncomeDto request,
            CancellationToken token = default);

        Task CreateFirstSalarySettingAsync(FirmId firmId, UserId userId, CreateFirstSalarySettingDto request,
            CancellationToken token = default);

        Task PublishForeignerEventsAsync(FirmId firmId, UserId userId, PublishForeignerEventsDto request,
            CancellationToken token = default);

        Task PublishEfsTdEventAsync(FirmId firmId, UserId userId, PublishEfsTdEventDto request,
            CancellationToken token = default);

        Task PublishWorkerEventAsync(FirmId firmId, UserId userId, CancellationToken token = default);

        Task<string> GetLastNumberByOrderTypeAsync(FirmId firmId, UserId userId, WorkerOrderType orderType, int year,
            CancellationToken token = default);

        Task DeleteAsync(FirmId firmId, UserId userId, int workerId, long? subcontoId,
            CancellationToken token = default);
    }
}