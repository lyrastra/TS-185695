using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.ErptV2.Client.EdsApi;
using Moedelo.ErptV2.Dto.Eds;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.ErptV2.Client
{
    public interface IEdsApiClient : IDI
    {
        Task<List<string>> ValidateEdsRequest(int firmId, int userId);
        Task<EdsResponse> SendEdsRequest(int firmId, int userId, EdsRequest request);

        Task<ERptStatusResponse> GetERptStatus(int firmId);
        Task<IEnumerable<ERptStatusResponse>> GetERptStatus(IEnumerable<int> firmIds);

        Task NotifyAboutEdsCreated (int firmId, int userId);
        Task NotifyAboutEdsCreateFailed(int firmId, int userId, int historyId);
        Task NotifyAboutEdsProlongation (int firmId, int userId, DateTime sendDate, bool isPayed, string errors);

        Task<ChangeCodeOfOutgoingDirectionResponse> ChangeCodeOfOutgoingDirection(int firmId, int userId, ChangeCodeOfOutgoingDirectionRequest request);
    }

    [InjectAsSingleton]
    public class EdsApiClient : BaseApiClientWithEndpoint, IEdsApiClient
    {
        public EdsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository) : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository.Get("ErptApiEndpoint"))
        {
        }

        public Task<ChangeCodeOfOutgoingDirectionResponse> ChangeCodeOfOutgoingDirection(int firmId, int userId, ChangeCodeOfOutgoingDirectionRequest request) =>
            PostAsync<ChangeCodeOfOutgoingDirectionRequest, ChangeCodeOfOutgoingDirectionResponse>(
                $"/eds/ChangeCodeOfOutgoingDirection?firmId={firmId}&userId={userId}", 
                request,
                new HttpQuerySetting
                {
                    Timeout = new TimeSpan(0, 10, 0)
                });

        public Task<ERptStatusResponse> GetERptStatus(int firmId) =>
            GetAsync<ERptStatusResponse>($"/eds/GetErStatus/{firmId}");

        public Task<IEnumerable<ERptStatusResponse>> GetERptStatus(IEnumerable<int> firmIds) =>
            PostAsync<ERptStatusRequest, IEnumerable<ERptStatusResponse>>($"/eds/eRptStatus", new ERptStatusRequest { FirmIds = firmIds });

        public Task NotifyAboutEdsCreated(int firmId, int userId) =>
            PostAsync($"/ErptNotification/EdsCreated?firmId={firmId}&userId={userId}");

        public Task NotifyAboutEdsCreateFailed(int firmId, int userId, int edsHistoryId) =>
            PostAsync($"/ErptNotification/EdsDocumentsRejected?firmId={firmId}&userId={userId}&edsHistoryId={edsHistoryId}");

        public Task NotifyAboutEdsProlongation(int firmId, int userId, DateTime sendDate, bool isPayed, string errors) =>
            PostAsync($"/ErptNotification/EdsProlongation?firmId={firmId}&userId={userId}&sendDate={sendDate.ToShortDateString()}&isPayed={isPayed}", errors);

        public Task<EdsResponse> SendEdsRequest(int firmId, int userId, EdsRequest request) =>
            PostAsync<EdsRequest, EdsResponse>($"/eds/SendEdsRequestV2?firmId={firmId}&userId={userId}", request);

        public Task<List<string>> ValidateEdsRequest(int firmId, int userId) =>
            GetAsync<List<string>>($"/eds/ValidateEdsRequestV2?firmId={firmId}&userId={userId}");
    }
}
