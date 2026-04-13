using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.PayrollV2.Dto.Postings;

namespace Moedelo.PayrollV2.Client.Postings
{
    [InjectAsSingleton]
    public class PostingApiV2Client : BaseApiClient, IPostingApiV2Client
    {
        private readonly HttpQuerySetting defaultSetting = new HttpQuerySetting(TimeSpan.FromMinutes(10));
        
        private readonly SettingValue apiEndPoint;
        
        public PostingApiV2Client(
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
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        /// <inheritdoc />
        public Task<SalaryPostingsModelV2Dto> GetPostingsAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<SalaryPostingsModelV2Dto>("/Postings",
                new
                {
                    firmId,
                    userId,
                    startDate = startDate.ToString("yyyy-MM-dd"),
                    endDate = endDate.ToString("yyyy-MM-dd")
                }, null, setting: new HttpQuerySetting(TimeSpan.FromMinutes(15)));
        }

        /// <inheritdoc />
        public Task<List<SalaryPostingDto>> ProvideAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return PostAsync<object, List<SalaryPostingDto>>(
                $"/Postings/Provide?firmId={firmId}&userId={userId}",
                new PeriodRequestDto
                {
                    StartDate = startDate, 
                    EndDate = endDate
                }, null, setting: new HttpQuerySetting(TimeSpan.FromMinutes(15)));
        }

        /// <inheritdoc />
        public Task<List<SalaryPostingForBizDto>> GetPostingsForBizAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate)
        {
            return GetAsync<List<SalaryPostingForBizDto>>("/Postings/GetForBiz",
                new
                {
                    firmId,
                    userId,
                    startDate = startDate.ToString("yyyy-MM-dd"),
                    endDate = endDate.ToString("yyyy-MM-dd")
                }, null, setting: new HttpQuerySetting(TimeSpan.FromMinutes(15)));
            
        }

        protected override HttpQuerySetting DefaultHttpQuerySetting()
        {
            return defaultSetting;
        }
    }
}