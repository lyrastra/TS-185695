using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkContracts;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IWorkContractApiClient))]
    public class WorkContractApiClient : BaseLegacyApiClient, IWorkContractApiClient
    {
        public WorkContractApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<WorkContractApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<IReadOnlyCollection<WorkContractDto>> GetListAsync(WorkContractsCriteriaDto criteriaDto)
        {
            return PostAsync<WorkContractsCriteriaDto, IReadOnlyCollection<WorkContractDto>>(
                $"/WorkContracts/ContractsByCriteria",
                criteriaDto);
        }

        public Task<WorkerWorkContractDto> GetActualContractAsync(long id)
        {
            return GetAsync<WorkerWorkContractDto>(
                $"/WorkContracts/ActualContract/{id}");
        }

        public Task UpdateNumbersAsync(int firmId, int userId, List<WorkerContractNumberUpdatingDto> dto)
        {
            return PostAsync($"/WorkContracts/UpdateNumbers?firmId={firmId}&userId={userId}", dto);
        }

        public Task<bool> HasContractsWithSfrChargesAsync(int firmId, int userId, int workerId)
        {
            return GetAsync<bool>($"/WorkContracts/HasContractsWithSfrCharges", new { firmId, userId, workerId });
        }

        public Task<IReadOnlyCollection<WorkerContractSettingDto>> GetActualContractsByWorkerIdAsync(int firmId, int userId, int workerId)
        {
            return GetAsync<IReadOnlyCollection<WorkerContractSettingDto>>(
                $"/WorkContracts/ActualContracts/ByWorkerId/{workerId}", new { firmId, userId });
        }
    }
}