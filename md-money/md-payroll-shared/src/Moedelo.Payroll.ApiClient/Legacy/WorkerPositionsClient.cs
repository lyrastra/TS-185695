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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerPositions;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IWorkerPositionsClient))]
    internal  sealed class WorkerPositionsClient: BaseLegacyApiClient, IWorkerPositionsClient
    {
        public WorkerPositionsClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<WorkerPositionsClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task UpdateOrdersAsync(int firmId, int userId, List<PositionHistoryOrderUpdatingDto> dto)
        {
            return PostAsync($"/WorkerPositions/UpdateOrders?firmId={firmId}&userId={userId}", dto);
        }
        

        public Task<WorkerPositionOnFireDateDto> GetWorkerPositionOnFireDateAsync(int firmId, int userId, int workerId)
        {
            return GetAsync<WorkerPositionOnFireDateDto>($"/WorkerPositions/GetWorkerPositionOnFireDate?firmId={firmId}&userId={userId}&workerId={workerId}");
        }

        public Task AssignDivisionAsync(FirmId firmId, UserId userId, AssignDivisionDto dto, CancellationToken token = default)
        {
            return PostAsync($"/WorkerPositions/AssignDivision?firmId={firmId}&userId={userId}", dto,
                cancellationToken: token);
        }

        public Task<IReadOnlyCollection<WorkerPositionDto>> GetByCriteriaAsync(FirmId firmId, UserId userId,
            ByCriteriaRequestDto request, CancellationToken token)
        {
            return PostAsync<ByCriteriaRequestDto, IReadOnlyCollection<WorkerPositionDto>>(
                $"/WorkerPositions/GetByCriteria?firmId={firmId}&userId={userId}",
                request, cancellationToken: token);
        }

        public Task<IReadOnlyCollection<DepartmentPositionResponseDto>> GetDepartmentsWithPositionsAsync(FirmId firmId, 
            UserId userId)
        {
            return GetAsync<IReadOnlyCollection<DepartmentPositionResponseDto>>(
                $"/WorkerPositions/GetDepartmentsWithPositions?firmId={firmId}&userId={userId}");
        }
    }
}
