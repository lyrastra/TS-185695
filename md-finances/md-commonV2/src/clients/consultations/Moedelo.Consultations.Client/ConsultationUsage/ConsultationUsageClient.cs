using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Consultations.Dto.ConsultationUsage;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Consultations.Client.ConsultationUsage
{
    [InjectAsSingleton(typeof(IConsultationUsageClient))]
    public class ConsultationUsageClient : BaseApiClient, IConsultationUsageClient
    {
        private readonly SettingValue apiEndpoint;
        public static string ControllerName = "/ConsultationUsage/";

        public ConsultationUsageClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ConsultationsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}{ControllerName}";
        }

        public Task<ConsultationUsageResponseDto> GetAsync(int userId, int firmId)
        {
            return GetAsync<ConsultationUsageResponseDto>("Get", new { userId, firmId });
        }

        public Task<int> GetQuestionCountAsync(int userId, int firmId, QuestionCountRequestDto requestDto)
        {
            return PostAsync<QuestionCountRequestDto, int>($"GetQuestionCount?firmId={firmId}&userId={userId}", requestDto);
        }

        public Task<int> GetNotReadAnswersCountAsync(int userId, int firmId)
        {
            return GetAsync<int>("GetNotReadAnswersCount", new { userId, firmId });
        }

        public Task<UserConsultationStatsDto> GetUserStatsAsync(int userId, CancellationToken cancellationToken)
        {
            var uri = $"user/{userId}/stats";

            return GetAsync<UserConsultationStatsDto>(uri, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyCollection<UserConsultantDataDto>> GetLastUsersConsultantsDataAsync(int firmId, CancellationToken cancellationToken)
        {
            return GetAsync<IReadOnlyCollection<UserConsultantDataDto>>($"GetLastUsersConsultants", new { firmId },
                cancellationToken: cancellationToken);
        }
    }
}