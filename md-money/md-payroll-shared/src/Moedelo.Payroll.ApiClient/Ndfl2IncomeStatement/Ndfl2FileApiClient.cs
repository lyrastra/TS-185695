using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.File;
using Moedelo.Payroll.ApiClient.Abstractions.Ndfl2IncomeStatement;
using Moedelo.Payroll.Shared.Enums.Common;

namespace Moedelo.Payroll.ApiClient.Ndfl2IncomeStatement
{
    [InjectAsSingleton(typeof(INdfl2FileApiClient))]
    public class Ndfl2FileApiClient : BaseLegacyApiClient, INdfl2FileApiClient
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromSeconds(90));

        public Ndfl2FileApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<Ndfl2FileApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<FileResultDto> GetFileAsync(FirmId firmId, UserId userId, int year, int workerId, int? number,
            DocumentFileType fileFormat, CancellationToken token = default)
        {
            return GetAsync<FileResultDto>(
                uri: "/Ndfl2File",
                queryParams: new
                {
                    FirmId = firmId, UserId = userId, Year = year, WorkerId = workerId, Number = number,
                    FileFormat = fileFormat
                },
                setting: DefaultHttpSetting,
                cancellationToken: token);
        }
    }
}