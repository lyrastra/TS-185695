using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto;
using Moedelo.RptV2.Dto.UsnDeclaration;

namespace Moedelo.RptV2.Client.UsnDeclaration
{
    [InjectAsSingleton]
    public class UsnDeclarationClient : BaseApiClient, IUsnDeclarationClient
    {
        private readonly SettingValue apiEndpoint;
        
        public UsnDeclarationClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/UsnDeclarationApi";
        }

        public async Task<UsnDeclarationDto> GetByYearAsync(int firmId, int userId, int year)
        {
            var result = await GetAsync<DataWrapper<UsnDeclarationDto>>("/GetByYear", new { firmId, userId, year }).ConfigureAwait(false);
            return result.Data;
        }
         public async Task<UsnTaxProfitAndExpenseDataDto> GetUsnTaxProfitAndExpenseDataAsync(int firmId, int userId, int year)
        {
            var uri = $"/GetUsnTaxProfitAndExpenseData?firmId={firmId}&userId={userId}&year={year}";
            var res = await GetAsync<DataWrapper<UsnTaxProfitAndExpenseDataDto>>(uri).ConfigureAwait(false);
            return res.Data;
        }

        public async Task<UsnTaxProfitAndExpenseSumDto> GetUsnTaxProfitAndExpenseSumAsync(int firmId, int userId, UsnTaxProfitAndExpenseDataDto taxData)
        {
            var uri = $"/GetUsnTaxProfitAndExpenseSum?firmId={firmId}&userId={userId}";
            var res = await PostAsync<UsnTaxProfitAndExpenseDataDto, DataWrapper<UsnTaxProfitAndExpenseSumDto>>(uri, taxData).ConfigureAwait(false);
            return res.Data;
        }

        public async Task<UsnTaxProfitDataDto> GetUsnTaxProfitDataAsync(int firmId, int userId, int year, bool? hasEmployees)
        {
            var res = await GetAsync<DataWrapper<UsnTaxProfitDataDto>>("/GetUsnTaxProfitData", 
                    new { firmId, userId, year, hasEmployees}).ConfigureAwait(false);
            return res.Data;
        }

        public async Task<UsnTaxProfitSumDto> GetUsnTaxProfitSumAsync(int firmId, int userId, UsnTaxProfitDataDto taxData)
        {
            var uri = $"/GetUsnTaxProfitSum?firmId={firmId}&userId={userId}";
            var res = await PostAsync<UsnTaxProfitDataDto, DataWrapper<UsnTaxProfitSumDto>>(uri, taxData).ConfigureAwait(false);
            return res.Data;
        }

        public async Task<BankPaymentDto> GetBankPaymentAsync(int firmId, int userId, long id)
        {
            return (await GetAsync<DataWrapper<BankPaymentDto>>("/GetBankPayment", new { firmId, userId, id }).ConfigureAwait(false)).Data;
        }

        public async Task<UsnDeclarationDto> GetByIdAsync(int firmId, int userId, long id)
        {
            var result = await GetAsync<DataWrapper<UsnDeclarationDto>>("/Get", new { firmId, userId, id }).ConfigureAwait(false);
            return result.Data;
        }
    }
}
