using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.Funds;

namespace Moedelo.PayrollV2.Client.Funds
{
    [InjectAsSingleton]
    public class FundPaymentsCheckClient: BaseApiClient, IFundPaymentsCheckClient
    {
        private readonly SettingValue apiEndPoint;

        public FundPaymentsCheckClient(
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
            return apiEndPoint.Value + "/FundPayments";
        }

        public Task<bool> HasNonPayedFundPaymentsAsync(int firmId, int userId, FundsRegistryRequestDto requestDto)
        {
            return GetAsync<bool>("/HasNonPayedFundPayments", new
            {
                firmId, 
                userId,
                requestDto.StartDate,
                requestDto.EndDate,
                requestDto.IsFfoms,
                requestDto.IsPfr,
                requestDto.IsFssDisability,
                requestDto.IsFssInjured,
                requestDto.IsSfr
            });
        }
    }
}
