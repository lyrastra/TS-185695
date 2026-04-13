using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Manufacturing.Dto.ManufacturingReports;

namespace Moedelo.Manufacturing.Client.ManufacturingReports
{
    [InjectAsSingleton]
    public class ManufacturingReportsClient : BaseApiClient, IManufacturingReportsClient
    {
        private readonly SettingValue apiEndpoint;

        public ManufacturingReportsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ManufacturingApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<ManufacturingReportDto>> GetByCriterionAsync(int firmId, int userId,
            ManufacturingReportRequestDto request)
        {
            return PostAsync<ManufacturingReportRequestDto, List<ManufacturingReportDto>>(
                $"/ManufacturingReports/GetByCriterion?firmId={firmId}&userId={userId}", request);
        }

        public Task<List<ManufacturingReportFullDto>> GetManufacturingReportsByBaseIds(int firmId, int userId,
            IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<ManufacturingReportFullDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<ManufacturingReportFullDto>>(
                $"/ManufacturingReports/GetByBaseIds?firmId={firmId}&userId={userId}", ids);
        }

        public Task<List<ManufacturingReportFullDto>> GetManufacturingReportsByPeriodAsync(int firmId,
            DateTime? startDate, DateTime? endDate)
        {
            return GetAsync<List<ManufacturingReportFullDto>>(
                $"/ManufacturingReports/GetByPeriod?firmId={firmId}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
        }

        public Task<bool> ExistsForDivisionAsync(int firmId, int divisionId)
        {
            return GetAsync<bool>(
                $"/ManufacturingReports/ExistsForDivision?firmId={firmId}&divisionId={divisionId}");
        }

        public Task<List<long>> GetProductIdsInManufacturedProductReportsAsync(int firmId, int userId, ManufacturedProductReportsProductIdsListRequest request)
        {
            return PostAsync<ManufacturedProductReportsProductIdsListRequest, List<long>>(
                $"/ManufacturingReports/GetProductIdsInManufacturedProductReports?firmId={firmId}&userId={userId}", request);
        }
    }
}