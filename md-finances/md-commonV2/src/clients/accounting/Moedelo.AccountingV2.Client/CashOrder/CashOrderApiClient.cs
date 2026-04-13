using Moedelo.AccountingV2.Dto.CashOrder;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.CashOrder
{
    [InjectAsSingleton]
    public class CashOrderApiClient : BaseApiClient, ICashOrderApiClient
    {
        private readonly SettingValue apiEndPoint;

        public CashOrderApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint =  settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<FirmCashOrderDto>> GetListAsync(int firmId, int userId,
            CashOrderPaginationRequest paginationRequest, CancellationToken cancellationToken)
        {
            var uri = $"/CashOrder/GetList?firmId={firmId}&userId={userId}";

            return PostAsync<CashOrderPaginationRequest, List<FirmCashOrderDto>>(uri, paginationRequest, cancellationToken: cancellationToken);
        }

        public Task<List<FirmCashOrderDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds,
            CancellationToken cancellationToken)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<FirmCashOrderDto>());
            }

            var uri = $"/CashOrder/GetByBaseIds?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<FirmCashOrderDto>>(uri, baseIds, cancellationToken: cancellationToken);
        }

        public Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, HttpQuerySetting setting = default)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }
            
            return PostAsync($"/CashOrder/Provide?firmId={firmId}&userId={userId}", baseIds, setting: setting);
        }

        public Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }
            
            return PostAsync($"/CashOrder/DeleteOrders?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task<long> Save(int firmId, int userId, BudgetaryCashOrderDto dto)
        {
            return PostAsync<BudgetaryCashOrderDto, long>($"/BudgetaryPayments/Save?firmId={firmId}&userId={userId}", dto);
        }
    }
}