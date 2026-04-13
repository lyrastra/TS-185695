using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccPostings.Client
{
    [InjectAsSingleton]
    public class SyntheticAccountBalanceClient : BaseApiClient, ISyntheticAccountBalanceClient
    {
        private readonly SettingValue apiEndPoint;

        public SyntheticAccountBalanceClient(
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
            apiEndPoint = settingRepository.Get("AccPostingsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }       

        public Task<List<SyntheticAccountBalanceDto>> GetAccountBalanceOnDateAsync(
            int firmId,
            int userId,
            SyntheticAccountBalanceRequestDto requestDto)
        {
            if (requestDto.AccountCodes?.Any() != true)
            {
                return Task.FromResult(new List<SyntheticAccountBalanceDto>());
            }

            var url = $"/SyntheticAccountBalance/GetOnDate?firmId={firmId}&userId={userId}";

            return PostAsync<SyntheticAccountBalanceRequestDto, List<SyntheticAccountBalanceDto>>(url, requestDto);
        }

        public Task<List<AccountBalanceDivisionDto>> GetPostingBalanceFor20AccountingAndManufacturingCostItemAsync(
            int firmId,
            int userId,
            DateTime endDate,
            CostItemGroupCode code)
        {
            var url = $"/SyntheticAccountBalance/GetPostingBalanceFor20AccountingAndManufacturingCostItem";

            return GetAsync<List<AccountBalanceDivisionDto>>(url, new
            {
                firmId,
                userId,
                endDate = endDate.ToString("yyyy-MM-dd"),
                code
            });
        }
    }
}