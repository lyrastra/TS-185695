using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.TestData.Client.Dto;
using System.Threading.Tasks;

namespace Moedelo.TestData.Client
{
    [InjectAsSingleton]
    public class TestDataApiClient : BaseCoreApiClient, ITestDataApiClient
    {
        private readonly SettingValue apiEndPoint;

        public TestDataApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("TestDataApiEndpoint");
        }

        /// <summary>
        /// Возвращает настройки по фирме с тестовыми данными
        /// </summary>
        /// <param name="userId"></param>
        public Task<TestDataSettingDto> GetAsync(int userId)
        {
            return GetAsync<TestDataSettingDto>($"/{userId}");
        }

        /// <summary>
        /// Инициализирует фирму с тестовыми данными для пользователя
        /// </summary>
        public async Task<TestDataSettingDto> InitializeAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            return await PostAsync<TestDataSettingDto>("/Initialize", tokenHeaders);
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndPoint.Value}/api/v1/Data";
        }
    }
}