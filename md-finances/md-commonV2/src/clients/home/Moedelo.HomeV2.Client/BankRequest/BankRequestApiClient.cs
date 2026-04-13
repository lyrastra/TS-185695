using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.BankPartners;
using Moedelo.HomeV2.Dto.BankRequest;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.BankRequest
{
    [InjectAsSingleton]
    public class BankRequestApiClient : BaseApiClient, IBankRequestApiClient
    {
        private const string ControllerUri = "/Rest/BankRequest/";
        private readonly SettingValue apiEndPoint;

        public BankRequestApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        /// <inheritdoc cref="IBankRequestApiClient.SendOpenBankAccountRequestAsync"/>
        public Task<BankRequestResponseDto> SendOpenBankAccountRequestAsync(BankRequestDto requestDto)
        {
            return PostAsync<BankRequestDto, BankRequestResponseDto>("SendOpenBankAccountRequest", requestDto);
        }

        /// <inheritdoc cref="IBankRequestApiClient.GetRequestsByBankAsync"/>
        public Task<IReadOnlyCollection<SavedBankRequestDto>> GetRequestsByBankAsync(
            BankPartners bank,
            string startDate,
            string endDate)
        {
            return GetAsync<IReadOnlyCollection<SavedBankRequestDto>>("", new GetSavedBankRequestsDto
                {
                    Bank = bank,
                    StartDate = startDate,
                    EndDate = endDate
                });
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerUri;
        }
    }
}