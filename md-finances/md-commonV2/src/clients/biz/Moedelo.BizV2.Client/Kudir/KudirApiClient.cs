using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BizV2.Dto.Kudir;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BizV2.Client.Kudir
{
    [InjectAsSingleton]
    public class KudirApiClient : BaseApiClient, IKudirApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public KudirApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("BizPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<KudirDownloadResult> Download(int firmId, int userId, KudirDownloadDto dto)
        {
            var result = await PostAsync<KudirDownloadDto, KudirDownloadHttpResult>(
                $"/Kudir/Download?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);

            return new KudirDownloadResult
            {
                Filename = result.Filename,
                Content = Convert.FromBase64String(result.Content),
                MimeType = result.MimeType
            };
        }

        public async Task<List<KudirBudgetaryPaymentDto>> GetBudgetaryPayments(int firmId, int userId, int year)
        {
            return await GetAsync<List<KudirBudgetaryPaymentDto>>("/Kudir/GetBudgetaryPayments", new { firmId, userId, year }).ConfigureAwait(false);
        }

        public async Task<List<SickKudirDto>> GetKudirSicklistPayments(int firmId, int userId, int year)
        {
            return await GetAsync<List<SickKudirDto>>("/Kudir/GetSicklistPayments", new { firmId, userId, year} ).ConfigureAwait(false);
        }

        public async Task<QuarterlyTaxPostingsDto> GetQuarterlyTaxPostingsAsync(int firmId, int userId, int year, DateTime? onDate = null)
        {
            return await GetAsync<QuarterlyTaxPostingsDto>($"/Kudir/GetQuarterlyTaxPostings?firmId={firmId}&userId={userId}&year={year}&onDate={onDate?.ToString("yyyy-MM-dd")}").ConfigureAwait(false);
        }

        public Task<QuarterlyTaxPostingsDto> GetQuarterlyTaxPostingsWithPatentAsync(int firmId, int userId,
            int year, DateTime? onDate = null, CancellationToken ct = default)
        {
            var uri = $"/Kudir/GetQuarterlyTaxPostingsWithPatent?firmId={firmId}&userId={userId}&year={year}&onDate={onDate?.ToString("yyyy-MM-dd")}&isRound=false";

            return GetAsync<QuarterlyTaxPostingsDto>(uri, cancellationToken: ct);
        }

        public Task<FundPaymentSumForUsnIpDto> GetFundPaymentSumForUsnIpAsync(int firmId, int userId, int year, CancellationToken ct)
        {
            return GetAsync<FundPaymentSumForUsnIpDto>("/Kudir/FundPaymentSumForUsnIp", new { firmId, userId, year }, cancellationToken: ct);
        }
    }
}