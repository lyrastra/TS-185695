using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.BackofficeBilling.Bills;
using Moedelo.BillingV2.Dto.BackofficeBilling.Bills.BillRequest;
using Moedelo.BillingV2.Dto.BackofficeBilling.Cost;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.BackofficeBillingBills
{
    [InjectAsSingleton]
    public class BackofficeBillingBillsApiClient : BaseApiClient, IBackofficeBillingBillsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public BackofficeBillingBillsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        protected override string GetApiEndpoint() => apiEndPoint.Value;

        public Task<int> InvoiceBillAsync(BillRequestDto request)
        {
            return PostAsync<BillRequestDto, int>(
                "/BackofficeBilling/V2/Bill/Invoice", request);
        }

        public Task<BackofficeBillingBillResponseDto> InvoiceBillAndGetInfoAsync(BillRequestDto request)
        {
            return PostAsync<BillRequestDto, BackofficeBillingBillResponseDto>(
                "/BackofficeBilling/V2/Bill/InvoiceAndGetInfo", request);
        }

        public Task<CostsResponseDto> CalculateCostAsync(BillRequestDto request)
        {
            return PostAsync<BillRequestDto, CostsResponseDto>(
                "/BackofficeBilling/V2/Bill/Cost", request);
        }

        public Task<BackofficeBillingBillResponseDto> GetBackofficeBillByPrimaryBillIdAsync(int primaryBillId)
        {
            return GetAsync<BackofficeBillingBillResponseDto>($"/BackofficeBilling/V2/Bill/{primaryBillId}");
        }

        public Task<int> InvoiceAndSwitchOnAsync(BillRequestDto request)
        {
            return PostAsync<BillRequestDto, int>(
                "/BackofficeBilling/V2/Bill/InvoiceAndSwitchOn", request);
        }

    }
}