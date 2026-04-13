using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.ErptV2.Dto.FnsNeformalDocuments;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.NeformalDocuments
{
    [InjectAsSingleton]
    public class FnsNeformalDocumentsIonSenderClient : BaseApiClient, IFnsNeformalDocumentsIonSenderClient
    {
        private readonly SettingValue apiEndpoint;
        
        public FnsNeformalDocumentsIonSenderClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository) 
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser)
        {
            apiEndpoint = settingRepository.Get("MainAppUrl");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task SendIonAutoRequest() => PostAsync("/Documents/ReportsService/SendIonAutoRequest");
        
        public Task SendNeformalDocumentsIon(FnsNeformalDocumentsIonDto request)
        {
            var req = new
            {
                userId = request.UserId,
                firmId = request.FirmId,
                dateBegin = request.DateBegin.ToString("dd.MM.yyyy"),
                year = request.Year,
                type = request.Type,
                typeDoc = request.TypeDoc,
                taxcode = request.TaxCode,
                kpp  = request.Kpp
            };

            return PostAsync<object>(
                "/Documents/ReportsService/SendIonAuto", req);
        }
    }
}
