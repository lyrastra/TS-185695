using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.KontragentsV2.Client.DtoWrappers;
using Moedelo.KontragentsV2.Dto.Contacts;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentContactClient : BaseApiClient, IKontragentContactClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentContactClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<List<KontragentContactDto>> GetByKontragentsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            if (kontragentIds?.Any() != true)
            {
                return new List<KontragentContactDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<int>, DataDto<List<KontragentContactDto>>>(
                $"/Contact/GetByKontragents?firmId={firmId}&userId={userId}", 
                kontragentIds).ConfigureAwait(false);
            
            return result.Data;
        }

        public async Task<long> AddAsync(int firmId, int userId, KontragentContactDto contact)
        {
            var result = await PostAsync<KontragentContactDto, DataDto<long>>(
                $"/Contact/Add?firmId={firmId}&userId={userId}",
                contact).ConfigureAwait(false);
            
            return result.Data;
        }
    }
}