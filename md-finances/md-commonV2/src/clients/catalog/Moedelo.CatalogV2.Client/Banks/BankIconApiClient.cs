using Moedelo.Common.Enums.Enums.Catalog;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.CatalogV2.Client.Banks
{
    [InjectAsSingleton]
    public class BankIconApiClient : BaseApiClient, IBankIconApiClient
    {
        private readonly SettingValue apiEndPoint;

        public BankIconApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/BankIcon/V2";
        }

        public Task<IReadOnlyDictionary<int, string>> GetByBankIdsAsync(IReadOnlyCollection<int> bankIds)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, string>>("/GetByBankIds", bankIds);
        }

        public Task<IReadOnlyDictionary<BankRegistrationNumber, string>> GetByRegistrationNumberAsync(IReadOnlyCollection<BankRegistrationNumber> registrationNumbers)
        {
            return PostAsync<IReadOnlyCollection<BankRegistrationNumber>, IReadOnlyDictionary<BankRegistrationNumber, string>>("/GetByRegistrationNumbers", registrationNumbers);
        }
    }
}
