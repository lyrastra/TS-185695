using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentFavouritesClient : BaseApiClient, IKontragentFavouritesClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentFavouritesClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        /// <inheritdoc />
        public Task<List<KontragentBaseInfoDto>> ByMoneyAsync(int firmId, int userId, int count, DateTime startDate,
            DateTime endDate)
        {
            return GetAsync<List<KontragentBaseInfoDto>>(
                "/Favourites/ByMoney",
                new {firmId, userId, count, startDate, endDate});
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}