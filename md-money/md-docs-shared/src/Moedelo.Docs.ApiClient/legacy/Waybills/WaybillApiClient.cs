using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Docs.ApiClient.legacy.Waybills
{
    [InjectAsSingleton(typeof(IWaybillApiClient))]
    public class WaybillApiClient : IWaybillApiClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue accountingApiEndpoint;

        public WaybillApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            ISettingRepository settingRepository)
        {
            this.httpRequestExecuter = httpRequestExecuter;
            accountingApiEndpoint = settingRepository.Get("AccountingApiEndpoint");
        }
        
        public async Task<List<WaybillDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<WaybillDto>();
            }
            
            var uri = $"{accountingApiEndpoint.Value}/WaybillApiV2/GetByBaseIds?firmId={firmId}&userId={userId}";
            var response = await httpRequestExecuter.PostAsync(uri, baseIds.ToUtf8JsonContent()).ConfigureAwait(false);
            return response.FromJsonString<List<WaybillDto>>();
        }
    }
}