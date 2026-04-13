using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FundsContributions;

namespace Moedelo.Payroll.ApiClient.NetFramework.FundsContributions
{
    [InjectAsSingleton(typeof(IFundsContributionsClient))]
    public class FundsContributionsClient : BaseApiClient, IFundsContributionsClient
    {
        private readonly SettingValue apiEndPoint;

        public FundsContributionsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/FundsContributions";
        }

        public Task<FundsContributionsDto> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<FundsContributionsDto>("/GetByPeriod", new
            {
                firmId,
                userId,
                startDate,
                endDate
            });
        }
    }
}
