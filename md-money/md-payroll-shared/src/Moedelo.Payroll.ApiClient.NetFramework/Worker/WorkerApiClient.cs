using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Audit;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FssRegistries;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges;
using Moedelo.Payroll.Shared.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.NetFramework.Worker
{
    [InjectAsSingleton(typeof(IWorkerApiClient))]
    public class WorkerApiClient : BaseApiClient, IWorkerApiClient
    {
        private readonly SettingValue apiEndPoint;

        public WorkerApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager
        )
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }


        public Task<IReadOnlyCollection<AuditWorkerDto>> GetWorkersByPeriod(int firmId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<FssRegistryWorkerInfoDto>> GetFssRegistryWorkersAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmploymentChangesWorkerDto>> GetEmploymentChangesWorkersAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<NotFiredWorkerAutocompleteResponseDto>> GetNotFiredWorkersAutocomplete(FirmId firmId, UserId userId, NotFiredWorkerAutocompleteRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<FinControlWorkerDto[]> GetForFinControlAsync(FirmId firmId, UserId userId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkersSummaryForOutsourceDto> GetWorkersSummaryForOutsourceAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(FirmId firmId, UserId userId, CreateWorkerDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<WorkerForInsuredFssAutocompleteDto>> GetWorkersForInsuredFssAutocompleteAsync(FirmId firmId, UserId userId,
            WorkerForInsuredFssAutocompleteRequestDto model)
        {
            throw new NotImplementedException();
        }

        public Task<WorkerForInsuredFssDto> GetWorkerForInsuredFssAsync(FirmId firmId, UserId userId, int workerId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNotFiredWorkerCountAsync(FirmId firmId, UserId userId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWorkingConditionsAsync(FirmId firmId, UserId userId, UpdateWorkingConditionsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<CommonWorkerDataDto> GetBySnilsAsync(FirmId firmId, string snils)
        {
            throw new NotImplementedException();
        }

        public Task<CommonWorkerDataDto> GetBySubcontoAsync(FirmId firmId, long subcontoId)
        {
            return GetAsync<CommonWorkerDataDto>("/Worker/GetBySubconto", new {firmId, subcontoId});
        }

        public Task<IReadOnlyCollection<CommonWorkerDataDto>> GetBySnilsAsync(FirmId firmId, IReadOnlyCollection<string> workersSnils)
        {
            throw new NotImplementedException();
        }

        public Task<List<EtrustWorkerAutocompleteDto>> GetEtrustWorkerAutocompleteAsync(int firmId)
        {
            throw new NotImplementedException();
        }

        public Task<GetWorkerDto> GetWorkerAsync(FirmId firmId, UserId userId, int workerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetWorkerDto>> GetWorkersByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds)
        {
            throw new NotImplementedException();
        }

        public Task<GetWorkerDto[]> GetListByFioAsync(FirmId firmId, UserId userId, string surname, string name, string patronymic)
        {
            throw new NotImplementedException();
        }

        public Task<GetWorkerDto[]> GetListByPassportAsync(FirmId firmId, UserId userId, string passportSerialAndNumber)
        {
            throw new NotImplementedException();
        }

        public Task<WorkerDataForRequisitesDto> GetDataAsync(FirmId firmId, UserId userId, int workerId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task ChangeStartDatePaymentSettingsAsync(FirmId firmId, UserId userId, ChangeStartDatePaymentSettingsDto request,
            CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWorkerYearlyIncomeAsync(FirmId firmId, UserId userId, UpdateWorkerYearlyIncomeDto request,
            CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task CreateFirstSalarySettingAsync(FirmId firmId, UserId userId, CreateFirstSalarySettingDto request,
            CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task PublishForeignerEventsAsync(FirmId firmId, UserId userId, PublishForeignerEventsDto request,
            CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task PublishEfsTdEventAsync(FirmId firmId, UserId userId, PublishEfsTdEventDto request,
            CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task PublishWorkerEventAsync(FirmId firmId, UserId userId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetLastNumberByOrderTypeAsync(FirmId firmId, UserId userId, WorkerOrderType orderType, int year,
            CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, int workerId, long? subcontoId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}