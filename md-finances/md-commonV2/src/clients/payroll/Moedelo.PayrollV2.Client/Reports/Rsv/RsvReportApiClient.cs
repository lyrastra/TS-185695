using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.Rsv;

namespace Moedelo.PayrollV2.Client.Reports.Rsv
{
    [InjectAsSingleton]
    public class RsvReportApiClient : BaseApiClient, IRsvReportApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public RsvReportApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }
        
        protected override string GetApiEndpoint()
        {
            return $"{apiEndPoint.Value}/RsvReport";
        }

        public Task<List<TariffDto>> GetPfrAndFfomsStepAsync(int firmId, int userId, int period, int year,
            IReadOnlyCollection<TariffDto> prevData = null)
        {
            return PostAsync<PfrAndFfomsStepRequestDto, List<TariffDto>>(
                $"/GetPfrAndFfomsStep?firmId={firmId}&userId={userId}",
                new PfrAndFfomsStepRequestDto {Period = period, Year = year, PrevData = prevData});
        }

        public Task<FssDto> GetFssStepAsync(int firmId, int userId, int period, int year, FssDto prevData = null)
        {
            return PostAsync<FssStepRequestDto, FssDto>($"/GetFssStep?firmId={firmId}&userId={userId}",
                new FssStepRequestDto {Period = period, Year = year, PrevData = prevData});
        }

        public Task<LowTariffDto> GetLowTariffStepAsync(int firmId, int userId, int period, int year, 
            LowTariffDto prevData = null)
        {
            return PostAsync<LowTariffStepRequestDto, LowTariffDto>(
                $"/GetLowTariffStep?firmId={firmId}&userId={userId}",
                new LowTariffStepRequestDto {Period = period, Year = year, PrevData = prevData});
        }

        public Task<List<WorkerInfoCheckDto>> GetWorkersForCheckAsync(int firmId, int userId, int period, int year)
        {
            return GetAsync<List<WorkerInfoCheckDto>>("/GetWorkersForCheck", new { firmId, userId, period, year });
        }

        public Task<List<WorkerInfoDto>> GetWorkersStepAsync(int firmId, int userId, int period, int year, 
            IReadOnlyCollection<WorkerInfoDto> prevData = null)
        {
            return PostAsync<WorkerStepRequestDto, List<WorkerInfoDto>>(
                $"/GetWorkersStep?firmId={firmId}&userId={userId}",
                new WorkerStepRequestDto {Period = period, Year = year, PrevData = prevData});
        }

        public Task<PatentLowTariffDto> UsePatentLowTariffAsync(int firmId, int userId, int period, int year)
        {
            return GetAsync<PatentLowTariffDto>("/UsePatentLowTariff", new { firmId, userId, period, year });
        }
    }
}