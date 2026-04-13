using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.CreateBill;
using Moedelo.BackofficeV2.Dto.Payments;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.Payments
{
    [InjectAsSingleton]
    public class PaymentClient : BaseApiClient, IPaymentClient
    {
        private const string ControllerUri = "/Rest/Payment/";
        private readonly SettingValue apiEndPoint;

        public PaymentClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint= settingRepository.Get("BackOfficePrivateApiEndpoint") ;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerUri;
        }

        public Task<List<OperationTypeDto>> GetOperationTypesAsync(int firmId)
        {
            return GetAsync<List<OperationTypeDto>>("GetOperationTypes", new {firmId});
        }

        public Task<List<PriceListCalculationDto>> GetPriceListCalculationsAsync(IEnumerable<PriceListCalculationRequestDto> calculationRequestDtos)
        {
            return PostAsync<IEnumerable<PriceListCalculationRequestDto>, List<PriceListCalculationDto>>( "GetPriceListCalculations", calculationRequestDtos);
        }
    }
}