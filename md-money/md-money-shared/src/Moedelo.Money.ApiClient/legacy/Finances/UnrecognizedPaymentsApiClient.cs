using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.legacy.Finances;
using Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.UnrecognizedPayments;

namespace Moedelo.Money.ApiClient.legacy.Finances
{
    [InjectAsSingleton(typeof(IUnrecognizedPaymentsApiClient))]
    internal class UnrecognizedPaymentsApiClient : BaseApiClient, IUnrecognizedPaymentsApiClient
    {
        public UnrecognizedPaymentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UnrecognizedPaymentsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("FinancesPrivateApiEndpoint"),
                logger)
        {
        }

        public Task<UnrecognizedMoneyTableResponseDto> GetUnrecognizedTableAsync(int firmId, int userId, MoneyTableRequestDto request)
        {
            return PostAsync<MoneyTableRequestDto, UnrecognizedMoneyTableResponseDto>($"/Money/UnrecognizedPayments/GetUnrecognizedTable?firmId={firmId}&userId={userId}", request);
        }
    }
}
