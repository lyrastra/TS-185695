using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using Moedelo.Money.Dto.PaymentOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders
{
    [InjectAsSingleton(typeof(IOutsourceApproveApiClient))]
    public class OutsourceApproveApiClient : BaseCoreApiClient, IOutsourceApproveApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1/PaymentOrders/Outsource/Approve";

        public OutsourceApproveApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("MoneyApiEndpoint").Value;
        }

        public async Task<OutsourceApproveDto[]> GetIsApprovedAsync(
            int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() == false)
            {
                return Array.Empty<OutsourceApproveDto>();
            }
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<OutsourceApproveDto[]>>(
                 $"{prefix}/GetByIds", documentBaseIds, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
