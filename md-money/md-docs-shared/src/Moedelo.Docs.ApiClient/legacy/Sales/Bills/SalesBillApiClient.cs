using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Bills;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Docs.ApiClient.legacy.Sales.Bills
{
    [InjectAsSingleton(typeof(ISalesBillApiClient))]
    public class SalesBillApiClient : ISalesBillApiClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue docsApiEndpoint;

        public SalesBillApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            ISettingRepository settingRepository)
        {
            this.httpRequestExecuter = httpRequestExecuter;
            docsApiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }

        public async Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<PaidSumDto>();
            }

            try
            {
                var uri = $"{docsApiEndpoint.Value}/SalesBills/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}";
                var response = await httpRequestExecuter.PostAsync(uri, baseIds.ToUtf8JsonContent()).ConfigureAwait(false);
                return response.FromJsonString<List<PaidSumDto>>();
            }
            catch (HttpRequestResponseStatusException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<PaidSumDto>();
                }
                throw;
            }
        }
    }
}