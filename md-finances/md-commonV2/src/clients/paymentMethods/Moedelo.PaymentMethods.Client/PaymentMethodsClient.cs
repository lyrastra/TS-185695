using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PaymentMethods.Dto;

namespace Moedelo.PaymentMethods.Client
{
    [InjectAsSingleton(typeof(IPaymentMethodsClient))]
    public class PaymentMethodsClient: BaseApiClient, IPaymentMethodsClient
    {
        private const string ControllerUri = "/Rest/PaymentMethods/";
        private readonly SettingValue apiEndPoint;

        public PaymentMethodsClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BillingPaymentMethodsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerUri;
        }

        public Task<PaymentMethodsRangeDto> GetFilteredRangeAsync(GetFilteredRangeRequestDto request)
        {
            return PostAsync<GetFilteredRangeRequestDto, PaymentMethodsRangeDto>("GetFilteredRange", request);
        }

        public Task UpdateAsync(UpdatePaymentMethodDto dto)
        {
            return PostAsync("Update", dto);
        }
        
        public Task<CreatePaymentMethodReponseDto> CreateAsync(CreatePaymentMethodDto dto)
        {
            return PostAsync<CreatePaymentMethodDto, CreatePaymentMethodReponseDto>("Create", dto);
        }

        public Task<int> GetNextIdAsync()
        {
            return GetAsync<int>("GetNextId");
        }

        public Task<PaymentMethodDto[]> GetAllAsync()
        {
            return GetAsync<PaymentMethodDto[]>("GetAll");
        }

        public Task<PaymentMethodDto[]> GetByCriteriaAsync(PaymentMethodSearchCriteriaDto dto)
        {
            return PostAsync<PaymentMethodSearchCriteriaDto, PaymentMethodDto[]>("GetByCriteria", dto);
        }

        public Task<PaymentMethodDto[]> GetCodeAutocompleteAsync(string code)
        {
            return GetAsync<PaymentMethodDto[]>("GetCodeAutocomplete", new { code });
        }

        public Task DisableAsync(string code)
        {
            return PostAsync($"Disable?code={code}");
        }

        public Task EnableAsync(string code)
        {
            return PostAsync($"Enable?code={code}");
        }
    }
}