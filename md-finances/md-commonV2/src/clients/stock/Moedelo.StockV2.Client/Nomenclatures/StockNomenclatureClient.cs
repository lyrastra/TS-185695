using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto.Nomenclatures;

namespace Moedelo.StockV2.Client.Nomenclatures
{
    [InjectAsSingleton]
    public class StockNomenclatureClient  : BaseApiClient, IStockNomenclatureClient
    {
        private readonly SettingValue apiEndPoint;

        public StockNomenclatureClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : 
            base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<StockNomenclatureDto>> GetAllAsync(int firmId, int userId)
        {
            var response = await GetAsync<ListResponse<StockNomenclatureDto>>(
                $"/StockNomenclature/Get", new {firmId, userId}).ConfigureAwait(false);

            return response.Items;
        }

        public Task CreateDefaultsAsync(int firmId, int userId)
        {
            return PostAsync<List<StockNomenclatureDto>>(
                $"/StockNomenclature/CreateDefaults?firmId={firmId}&userId={userId}");
        }

        public async Task<long?> Save(int firmId, int userId, StockNomenclatureDto nomenclature)
        {
            var result = await PostAsync<StockNomenclatureDto, SavedLongId>(
                $"/StockNomenclature/Save?firmId={firmId}&userId={userId}",
                nomenclature).ConfigureAwait(false);

            return result.SavedId;
        }
    }
}