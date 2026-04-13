using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.RequisitesV2.Dto.FirmTaxationSystem;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;
using System.Threading;

namespace Moedelo.RequisitesV2.Client.FirmTaxationSystem
{
    [InjectAsSingleton(typeof(IFirmTaxationSystemClient))]
    public class FirmTaxationSystemClient : BaseApiClient, IFirmTaxationSystemClient
    {
        private readonly SettingValue apiEndPoint;

        public FirmTaxationSystemClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<FirmTaxationSystemDto> GetTaxationSystemAsync(int firmId, int year,
            CancellationToken cancellationToken)
        {
            const string uri = "/FirmTaxationSystem/Get";
            var queryParams = new { firmId, year }; 
            
            var result = await GetAsync<FirmTaxationSystemResponse>(
                uri, queryParams, cancellationToken: cancellationToken).ConfigureAwait(false);

            return result.Data;
        }

        public Task<List<TaxationSystemDto>>GetAsync(int firmId, int userId)
        {
            return GetAsync<List<TaxationSystemDto>>("/TaxationSystem", new { firmId, userId });
        }

        public Task<List<TaxationSystemFirmDto>> GetAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, List<TaxationSystemFirmDto>>("/TaxationSystem/GetByFirmIds", firmIds);
        }

        public Task<List<ActualFirmTaxationSystemDto>>GetActualAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, List<ActualFirmTaxationSystemDto>>("/TaxationSystem/Actual", firmIds);
        }

        public Task<TaxationSystemDto> GetByYearAsync(int firmId, int userId, int year,
            CancellationToken cancellationToken = default)
        {
            return GetAsync<TaxationSystemDto>($"/TaxationSystem/{year}",
                new { firmId, userId },
                cancellationToken: cancellationToken);
        }

        public Task SaveAsync(int firmId, int userId, TaxationSystemDto taxationSystem)
        {
            return PostAsync($"/TaxationSystem?firmId={firmId}&userId={userId}", taxationSystem, setting: new HttpQuerySetting() { Timeout = new TimeSpan(0, 2, 0) });
        }
    }
}