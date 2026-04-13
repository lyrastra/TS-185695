using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.PayrollV2.Dto;
using Moedelo.PayrollV2.Dto.Funds;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.PayrollV2.Client.Funds
{
    [InjectAsSingleton]
    public class FundsClient : BaseApiClient, IFundsClient
    {
        private readonly SettingValue apiEndPoint;

        public FundsClient(
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
            apiEndPoint = settingRepository.Get("PayrollApi");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndPoint.Value}/FundsRestApi";
        }

        /// <summary>
        /// Получить начисленные и уплаченные взносы в фонды за период
        /// (Период должен быть в пределах одного года)
        /// </summary>
        public async Task<decimal> GetAssessedAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return (await PostAsync<object, DataResponse<decimal>>($"/GetAssessed?firmId={firmId}&userId={userId}",
                    new
                    {
                        startDate = startDate.ToShortDateString(),
                        endDate = endDate.ToShortDateString()
                    })
                    .ConfigureAwait(false)).Data;
        }

        public async Task<List<FundChargesSummaryDto>> GetAssessedToPfrAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return (await PostAsync<object, ListResponse<FundChargesSummaryDto>>($"/WorkersForSzvm?firmId={firmId}&userId={userId}",
                new
                {
                    startDate = startDate.ToShortDateString(),
                    endDate = endDate.ToShortDateString()
                })
                .ConfigureAwait(false)).Items;
        }

        public async Task<decimal> GetEmployeesChargeAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return (await PostAsync<object, DataResponse<decimal>>($"/GetEmployeesCharge?firmId={firmId}&userId={userId}", 
                new
                {
                    startDate = startDate.ToShortDateString(),
                    endDate = endDate.ToShortDateString()
                }).ConfigureAwait(false))
                .Data;
        }
    }
}