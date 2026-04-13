using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.TaxPostings.Client.Postings.Money.Extensions;
using Moedelo.TaxPostings.Dto.Postings.Dto;
using Moedelo.TaxPostings.Dto.Postings.Money.Incoming.Dto;
using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Client.Postings.Money.Incoming
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerTaxPostingsApiClient))]
    class PaymentFromCustomerTaxPostingsApiClient : BaseCoreApiClient, IPaymentFromCustomerTaxPostingsApiClient
    {
        private readonly ISettingRepository settingRepository;

        public PaymentFromCustomerTaxPostingsApiClient(
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
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("TaxPostingsMoneyApiEndpoint").Value;
        }

        public async Task<ITaxPostingsResponseDto<ITaxPostingDto>> GenerateAsync(int firmId, int userId, PaymentFromCustomerGenerateRequestDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var uri = $"/api/v1/PaymentOrders/Incoming/PaymentFromCustomer";
            var response = await PostAsync<PaymentFromCustomerGenerateRequestDto, dynamic>(
                uri,
                request,
                queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
            return TaxPostingsResponseMapper.MapToDto(response);
        }
    }
}
