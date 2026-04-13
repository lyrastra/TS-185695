using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.FirmImages;

namespace Moedelo.RequisitesV2.Client.FirmImages
{
    [InjectAsSingleton]
    public class FirmImageClient : BaseApiClient, IFirmImageClient
    {
        private readonly SettingValue apiEndPoint;
        
        public FirmImageClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<Dictionary<FirmImageType, byte[]>> GetAsync(int firmId, int userId, IReadOnlyCollection<FirmImageType> types)
        {
            if (types?.Any() != true)
            {
                return Task.FromResult(new Dictionary<FirmImageType, byte[]>());
            }

            return PostAsync<IEnumerable<FirmImageType >, Dictionary <FirmImageType, byte[]>>(
                $"/FirmImages/GetByTypes?firmId={firmId}&userId={userId}",
                types);
        }

        /// <summary>
        /// Возвращает типы существующих картинок для конкретной компании (логотип, печать, подпись, подпись глбуха)
        /// без самих картинок
        /// </summary>
        public Task<List<FirmImageType>> GetImageTypesAsync(int firmId, int userId)
        {
            return GetAsync<List<FirmImageType>>(
                $"/FirmImages/GetFirmImageTypes?firmId={firmId}&userId={userId}");
        }
        
        public Task<Dictionary<FirmImageType, FirmImageWithOffsetDto>> GetImagesWithOffsetsAsync(int firmId, int userId, IReadOnlyCollection<FirmImageType> types)
        {
            if (types?.Any() != true)
            {
                return Task.FromResult(new Dictionary<FirmImageType, FirmImageWithOffsetDto>());
            }

            return PostAsync<IEnumerable<FirmImageType>, Dictionary <FirmImageType, FirmImageWithOffsetDto>>(
                $"/FirmImages/GetByTypesWithOffsets?firmId={firmId}&userId={userId}",
                types);
        }
    }
}