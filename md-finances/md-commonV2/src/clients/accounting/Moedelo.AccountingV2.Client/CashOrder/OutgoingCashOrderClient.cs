using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.CashOrder;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.CashOrder
{
    [InjectAsSingleton]
    public class OutgoingCashOrderClient : BaseApiClient, IOutgoingCashOrderClient
    {
        private readonly SettingValue apiEndPoint;
        
        public OutgoingCashOrderClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<CashOrderBaseInfoDto>> GetBaseInfoByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<CashOrderBaseInfoDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<CashOrderBaseInfoDto>>(
                $"/OutgoingCashOrder/GetList?firmId={firmId}&userId={userId}",
                baseIds);
        }

        public Task<long> CreateAsync(int firmId, int userId, NewRetailRefundOrderDto dto)
        {
            return PostAsync<NewRetailRefundOrderDto, long>(
                $"/OutgoingCashOrder/CreateRetailRefundOrder?firmId={firmId}&userId={userId}",
                dto);
        }
    }
}