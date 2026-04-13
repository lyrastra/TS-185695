using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.MiddlemanReports;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Docs.ApiClient.legacy.Sales.MiddlemanReports
{
    [InjectAsSingleton(typeof(IMiddlemanReportClient))]
    public class MiddlemanReportClient : IMiddlemanReportClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue docsApiEndpoint;

        public MiddlemanReportClient(
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

            var uri = $"{docsApiEndpoint.Value}/MiddlemanReports/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}";
            var response = await httpRequestExecuter.PostAsync(uri, baseIds.ToUtf8JsonContent()).ConfigureAwait(false);
            return response.FromJsonString<List<PaidSumDto>>();
        }
    }
}