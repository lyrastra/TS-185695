using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.CustomCommonEvents;

namespace Moedelo.RequisitesV2.Client.CustomCommonEvents
{
    [InjectAsSingleton(typeof(ICustomCommonEventApiClient))]
    public class CustomCommonEventApiClient : BaseApiClient, ICustomCommonEventApiClient
    {
        private readonly SettingValue apiEndPoint;

        public CustomCommonEventApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
            httpRequestExecutor, 
            uriCreator, 
            responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<CustomCommonEventResponseDto> GetAsync(int id)
        {
            return GetAsync<CustomCommonEventResponseDto>($"/CustomCommonEvent/{id}");
        }

        public Task<CustomCommonEventResponseDto> CreateAsync(int userId, CustomCommonEventSaveDto dto)
        {
            return PostAsync<CustomCommonEventSaveDto, CustomCommonEventResponseDto>($"/CustomCommonEvent?userId={userId}", dto);
        }

        public Task<CustomCommonEventResponseDto> UpdateAsync(int userId, int id, CustomCommonEventSaveDto dto)
        {
            return PostAsync<CustomCommonEventSaveDto, CustomCommonEventResponseDto>($"/CustomCommonEvent/Update/{id}?userId={userId}", dto);
        }

        public Task DeleteAsync(int id)
        {
            return DeleteAsync($"/CustomCommonEvent/{id}");
        }

        public Task<IReadOnlyList<CustomCommonEventTableItemResponseDto>> GetAsync()
        {
            return GetAsync<IReadOnlyList<CustomCommonEventTableItemResponseDto>>($"/CustomCommonEvent/All");
        }
    }
}