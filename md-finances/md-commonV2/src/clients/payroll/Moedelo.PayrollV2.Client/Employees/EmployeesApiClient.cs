using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.Employees;
using Moedelo.PayrollV2.Dto.Positions;
using Moedelo.PayrollV2.Dto.SzvmReport;
using Moedelo.PayrollV2.Dto;

namespace Moedelo.PayrollV2.Client.Employees
{
    [InjectAsSingleton(typeof(IEmployeesApiClient))]
    public class EmployeesApiClient : BaseApiClient, IEmployeesApiClient
    {
        private readonly SettingValue apiEndPoint;

        public EmployeesApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollApi");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndPoint.Value}/EmployeesRestApi";
        }

        public async Task<WorkerDto> GetWorkerAsync(int firmId, int userId, int workerId,
            CancellationToken cancellationToken)
        {
            var uri = $"/GetWorker?firmId={firmId}&userId={userId}";
            var response = await PostAsync<object, DataResponse<WorkerDto>>(
                uri, new { workerId }, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<WorkerDto>> GetWorkersAsync(int firmId, int userId, IReadOnlyCollection<int> workerIds,
            CancellationToken cancellationToken)
        {
            if (workerIds.Count == 0)
            {
                return new List<WorkerDto>();
            }

            var response = await PostAsync<IReadOnlyCollection<int>, WorkerListDto>(
                $"/GetWorkers?firmId={firmId}&userId={userId}", workerIds, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return response?.Workers ?? new List<WorkerDto>();
        }

        public Task<WorkerCardAccountDto> GetWorkerCardAccount(int firmId, int userId, int workerId)
        {
            return PostAsync<object, WorkerCardAccountDto>(
                $"/GetWorkerCardAccount?firmId={firmId}&userId={userId}", new { workerId });
        }

        public Task<decimal> GetAverageEmployeesCount(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return PostAsync<object, decimal>(
                $"/GetAverageEmployeesCount2016?firmId={firmId}&userId={userId}",
                new { startDate, endDate });
        }

        public Task<decimal> GetAverageEmployeesCountForFss(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return PostAsync<object, decimal>(
                $"/GetAverageEmployeesCountForFss2016?firmId={firmId}&userId={userId}",
                new { startDate, endDate });
        }

        public async Task<List<WorkerAutocompleteDto>> GetAutocompleteAsync(int firmId, int userId, string query,
            int count = 5, CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<ListResponse<WorkerAutocompleteDto>>(
                "/Autocomplete", new { firmId, userId, query, count }, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return response.Items;
        }

        public Task<WorkerAccountSettingAndDivisionSubcontoDto> GetWorkerAccountSettingOnDateAsync(int firmId, int userId, int workerId, DateTime date)
        {
            return PostAsync<object, WorkerAccountSettingAndDivisionSubcontoDto>(
                $"/GetWorkerSyntheticAccountCodeOnDate?firmId={firmId}&userId={userId}",
                new
                {
                    workerId,
                    date = date.ToShortDateString()
                });
        }

        public Task<List<WorkerAccountSettingAndDivisionSubcontoDto>> GetWorkersAccountSettingOnDateAsync(int firmId,
            int userId, IEnumerable<int> workerIds, DateTime date)
        {
            return PostAsync<object, List<WorkerAccountSettingAndDivisionSubcontoDto>>(
                $"/GetWorkersSyntheticAccountCodeOnDate?firmId={firmId}&userId={userId}",
                new
                {
                    workerIds,
                    date = date.ToShortDateString()
                });
        }

        public async Task<WorkerDto> GetDirectorAsync(int firmId, int userId)
        {
            var result = await GetAsync<DataResponse<WorkerDto>>("/GetDirector", new { firmId, userId }).ConfigureAwait(false);

            return result.Data;

        }

        public async Task<TaxationSystemType> GetWorkerTaxationSystemOnDateAsync(int firmId, int userId, int workerId, DateTime date)
        {
            var result = await PostAsync<object, DataResponse<TaxationSystemType>>($"/GetWorkerTaxationSystemOnDate?firmId={firmId}&userId={userId}", new { workerId, date = date.ToShortDateString() })
                .ConfigureAwait(false);

            return result.Data;

        }

        public Task<WorkerPositionFullDto> GetWorkersDivisionByDate(int firmId, int userId, int workerId, DateTime date)
        {
            return PostAsync<object, WorkerPositionFullDto>($"/GetWorkersDivisionByDate?firmId={firmId}&userId={userId}", new { workerId, date = date.ToShortDateString() });

        }

        public async Task<List<NotFiredWorkerDto>> GetNotFiredWorkersAsync(int firmId, int userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken)
        {
            var uri = $"/GetNotFiredWorkers?firmId={firmId}&userId={userId}";
            var queryParams = new { startDate, endDate };

            var results = await PostAsync<object, DataResponse<List<NotFiredWorkerDto>>>(uri, queryParams, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return results?.Data ?? new List<NotFiredWorkerDto>();

        }

        public async Task ResaveWithSubcontoAsync(int userId, int firmId)
        {
            await PostAsync<DataResponse<List<WorkerForSzvmDto>>>($"/ResaveWithSubconto?firmId={firmId}&userId={userId}", setting: new HttpQuerySetting() { Timeout = new TimeSpan(0, 2, 0) })
                .ConfigureAwait(false); ;
        }

        public Task UpdateAdvanceForBusinessTrip(int firmId, int userId, long documentBaseId, decimal advanceSum)
        {
            return PostAsync<object>($"/UpdateAdvanceForBusinessTrip?firmId={firmId}&userId={userId}", new { documentBaseId, advanceSum });
        }

        public async Task<IList<DivisionDto>> GetDivisionsForFirmAsync(int firmId, int userId)
        {
            var results = await PostAsync<ListResponse<DivisionDto>>($"/GetDivisionsForFirm?firmId={firmId}&userId={userId}").ConfigureAwait(false);

            return results.Items;

        }

        public async Task<int> GetStaffCountAsync(int firmId, int userId)
        {
            var results = await GetAsync<DataResponse<int>>("/GetStaffCount", new { userId, firmId }).ConfigureAwait(false);

            return results.Data;
        }

        public async Task<bool> IsPatentUsedByWorker(int firmId, int userId, long patentId, CancellationToken ct)
        {
            var results = await GetAsync<DataResponse<bool>>("/IsPatentUsedByWorker",
                new { firmId, userId, patentId },
                cancellationToken: ct).ConfigureAwait(false);

            return results.Data;
        }

        public async Task<WorkerForeignerStatusDto> GetWorkerForeignerStatusOnDate(int firmId, int userId, int workerId, DateTime date)
        {
            return await PostAsync<object, WorkerForeignerStatusDto>($"/GetWorkerForeignerStatusOnDate?firmId={firmId}&userId={userId}", new
            {
                workerId,
                date = date.ToShortDateString()
            }).ConfigureAwait(false);
        }

        public async Task<WorkerPositionDto> GetCurrentWorkerPosition(int firmId, int userId, int workerId)
        {
            return await PostAsync<object, WorkerPositionDto>($"/GetCurrentWorkerPosition?firmId={firmId}&userId={userId}", new
            {
                workerId
            }).ConfigureAwait(false);
        }

        public Task<List<WorkerPositionFullDto>> GetWorkersPositions(int firmId, int userId, IEnumerable<int> workerIds)
        {
            return PostAsync<object, List<WorkerPositionFullDto>>(
                $"/GetWorkersPositions?firmId={firmId}&userIds={userId}",
                new
                {
                    workerIds
                });
        }

        public async Task<List<WorkerMonthlySalaryDto>> GetWorkersAndMonthlySalaryAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            var result = await PostAsync<object, SalaryResponse>($"/WorkerSalaryForPeriod?firmId={firmId}&userId={userId}", new
            {
                startDate = startDate.ToShortDateString(),
                endDate = endDate.ToShortDateString()
            }).ConfigureAwait(false);
            return result.Salary;
        }

        public Task UpdatePatentInWorkerSettingAsync(int firmId, int userId, long patentId, DateTime? start, DateTime end)
        {
            return PostAsync($"/UpdatePatentInWorkerSetting?firmId={firmId}&userId={userId}", new { patentId, end, start });
        }

        public async Task<List<WorkerDto>> GetWorkersByFioAsync(int firmId, int userId, string surname,
            string name = null, string patronymic = null)
        {
            return (await PostAsync<object, ListResponse<WorkerDto>>(
                $"/GetWorkersByFio?firmId={firmId}&userId={userId}",
                new {surname, name, patronymic}).ConfigureAwait(false)).Items;
        }

        public Task<List<int>> GetWorkerIdsByPatentIdAsync(int firmId, int userId, long patentId)
        {
            return GetAsync<List<int>>("/GetWorkerIdsByPatentId", new { firmId, userId, patentId });
        }
    }
}
