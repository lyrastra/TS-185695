using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Audit;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FssRegistries;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges;
using Moedelo.Payroll.Shared.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IWorkerApiClient))]
    internal sealed class WorkerApiClient : BaseLegacyApiClient, IWorkerApiClient
    {
        public WorkerApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<WorkerApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"), 
                logger)
        {
        }

        public Task<IReadOnlyCollection<AuditWorkerDto>> GetWorkersByPeriod(
            int firmId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<IReadOnlyCollection<AuditWorkerDto>>("/Worker/GetWorkersByPeriod", new {firmId, startDate, endDate});
        }

        public Task<List<FssRegistryWorkerInfoDto>> GetFssRegistryWorkersAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds)
        {
            return PostAsync<WorkerListRequest, List<FssRegistryWorkerInfoDto>>($"/Worker/GetWorkersForFssRegistry?firmId={firmId}&userId={userId}",
                new WorkerListRequest(workerIds));
        }

        public Task<List<EmploymentChangesWorkerDto>> GetEmploymentChangesWorkersAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds)
        {
            return PostAsync<WorkerListRequest, List<EmploymentChangesWorkerDto>>($"/Worker/GetEmploymentChangesWorkers?firmId={firmId}&userId={userId}",
                new WorkerListRequest(workerIds));
        }

        public Task<IReadOnlyCollection<NotFiredWorkerAutocompleteResponseDto>> GetNotFiredWorkersAutocomplete(FirmId firmId, UserId userId, NotFiredWorkerAutocompleteRequestDto request)
        {
            return GetAsync<IReadOnlyCollection<NotFiredWorkerAutocompleteResponseDto>>(
                $"/Worker/GetNotFiredWorkersAutocomplete",
                new
                {
                    firmId,
                    userId,
                    request.Query,
                    request.Top
                }
            );
        }

        public Task<FinControlWorkerDto[]> GetForFinControlAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<FinControlWorkerDto[]>("/Worker/GetWorkersForFinControl", new {firmId, userId });
        }

        public Task<WorkersSummaryForOutsourceDto> GetWorkersSummaryForOutsourceAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<WorkersSummaryForOutsourceDto>("/Worker/GetWorkersSummaryForOutsource", new {firmId, userId, startDate, endDate});
        }
        
        public Task<int> CreateAsync(FirmId firmId, UserId userId, CreateWorkerDto model)
        {
            return PostAsync<CreateWorkerDto, int>($"/Worker/Create?firmId={firmId}&userId={userId}", model);
        }
        
        public Task<IReadOnlyCollection<WorkerForInsuredFssAutocompleteDto>> GetWorkersForInsuredFssAutocompleteAsync(
            FirmId firmId, UserId userId, WorkerForInsuredFssAutocompleteRequestDto model)
        {
            return PostAsync<WorkerForInsuredFssAutocompleteRequestDto,
                IReadOnlyCollection<WorkerForInsuredFssAutocompleteDto>>(
                $"/Worker/GetWorkersForInsuredFssAutocomplete?firmId={firmId}&userId={userId}", model);
        }

        public Task<WorkerForInsuredFssDto> GetWorkerForInsuredFssAsync(FirmId firmId, UserId userId, int workerId)
        {
            return GetAsync<WorkerForInsuredFssDto>($"/Worker/GetWorkerForInsuredFss", new {firmId, userId, workerId});
        }

        public Task<int> GetNotFiredWorkerCountAsync(FirmId firmId, UserId userId, DateTime date)
        {
            return GetAsync<int>("/Worker/GetNotFiredWorkerCount", new {firmId, userId, date});
        }

        public Task<CommonWorkerDataDto> GetBySnilsAsync(FirmId firmId, string snils)
        {
            return GetAsync<CommonWorkerDataDto>($"/Worker/GetBySnils", new {firmId, snils});
        }

        public Task<CommonWorkerDataDto> GetBySubcontoAsync(FirmId firmId, long subcontoId)
        {
            return GetAsync<CommonWorkerDataDto>($"/Worker/GetBySubconto", new {firmId, subcontoId});
        }
        
        public Task<IReadOnlyCollection<CommonWorkerDataDto>> GetBySnilsAsync(FirmId firmId,
            IReadOnlyCollection<string> workersSnils)
        {
            return PostAsync<IReadOnlyCollection<string>, IReadOnlyCollection<CommonWorkerDataDto>>(
                $"/Worker/GetBySnils?firmId={firmId}", workersSnils);
        }

        public Task UpdateWorkingConditionsAsync(FirmId firmId, UserId userId, UpdateWorkingConditionsDto dto)
        {
            return PostAsync($"/Worker/UpdateWorkingConditions?firmId={firmId}&userId={userId}", dto);
        }

        public Task<List<EtrustWorkerAutocompleteDto>> GetEtrustWorkerAutocompleteAsync(int firmId)
        {
            return GetAsync<List<EtrustWorkerAutocompleteDto>>($"/Worker/EtrustWorkerAutocomplete", new { firmId });
        }

        public Task<GetWorkerDto> GetWorkerAsync(FirmId firmId, UserId userId, int workerId)
        {
            return GetAsync<GetWorkerDto>("/Worker/GetWorker", new { firmId, userId, workerId });
        }
        
        public Task<List<GetWorkerDto>> GetWorkersByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds)
        {
            return PostAsync<IReadOnlyCollection<int>, List<GetWorkerDto>>($"/Worker/GetWorkersByIds?firmId={firmId}&userId={userId}", workerIds);
        }

        public Task<GetWorkerDto[]> GetListByFioAsync(FirmId firmId, UserId userId, string surname, string name, string patronymic)
        {
            return GetAsync<GetWorkerDto[]>($"/Worker/GetListByFio", new { firmId, userId, surname, name, patronymic });
        }

        public Task<GetWorkerDto[]> GetListByPassportAsync(FirmId firmId, UserId userId, string passportSerialAndNumber)
        {
            return GetAsync<GetWorkerDto[]>($"/Worker/GetListByPassport", new { firmId, userId, passportSerialAndNumber });
        }

        public Task<WorkerDataForRequisitesDto> GetDataAsync(FirmId firmId, UserId userId, int workerId, 
            DateTime? date = null, CancellationToken token = default)
        {
            return GetAsync<WorkerDataForRequisitesDto>("/Worker/DataForRequisites",
                new { firmId, userId, workerId, date }, cancellationToken: token);
        }

        public Task ChangeStartDatePaymentSettingsAsync(FirmId firmId, UserId userId, ChangeStartDatePaymentSettingsDto request,
            CancellationToken token = default)
        {
            return PostAsync($"/Worker/ChangeStartDatePaymentSettings?firmId={firmId}&userId={userId}", request,
                cancellationToken: token);
        }

        public Task UpdateWorkerYearlyIncomeAsync(FirmId firmId, UserId userId, UpdateWorkerYearlyIncomeDto request,
            CancellationToken token = default)
        {
            return PostAsync($"/Worker/UpdateWorkerYearlyIncome?firmId={firmId}&userId={userId}", request,
                cancellationToken: token);
        }

        public Task CreateFirstSalarySettingAsync(FirmId firmId, UserId userId, CreateFirstSalarySettingDto request,
            CancellationToken token = default)
        {
            return PostAsync($"/Worker/CreateFirstSalarySetting?firmId={firmId}&userId={userId}", request,
                cancellationToken: token);
        }

        public Task PublishForeignerEventsAsync(FirmId firmId, UserId userId, PublishForeignerEventsDto request,
            CancellationToken token = default)
        {
            return PostAsync($"/Worker/PublishForeignerEvents?firmId={firmId}&userId={userId}", request,
                cancellationToken: token);
        }

        public Task PublishEfsTdEventAsync(FirmId firmId, UserId userId, PublishEfsTdEventDto request,
            CancellationToken token = default)
        {
            return PostAsync($"/Worker/PublishEfsTdEvent?firmId={firmId}&userId={userId}", request,
                cancellationToken: token);
        }

        public Task PublishWorkerEventAsync(FirmId firmId, UserId userId, CancellationToken token = default)
        {
            return PostAsync($"/Worker/PublishWorkerEvent?firmId={firmId}&userId={userId}", cancellationToken: token);
        }

        public Task<string> GetLastNumberByOrderTypeAsync(FirmId firmId, UserId userId, WorkerOrderType orderType, int year,
            CancellationToken token = default)
        {
            return GetAsync<string>($"/Worker/GetLastNumberByOrderType", new { firmId, userId, orderType, year },
                cancellationToken: token);
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, int workerId, long? subcontoId, CancellationToken token = default)
        {
            return PostAsync(
                $"/Worker/Delete?firmId={firmId}&userId={userId}&workerId={workerId}&subcontoId={subcontoId}",
                cancellationToken: token);
        }
    }
}