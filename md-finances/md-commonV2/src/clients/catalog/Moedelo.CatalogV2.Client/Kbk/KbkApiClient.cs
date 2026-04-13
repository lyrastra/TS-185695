using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Kbk;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.KbkNumbers;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Kbk
{
    [InjectAsSingleton]
    public class KbkApiClient : BaseApiClient, IKbkApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public KbkApiClient(
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
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Kbk/V2";
        }

        /// <summary>
        /// Получение всех КБК (в т. ч. неактуальных)
        /// </summary>
        public Task<List<KbkNumberDto>> GetAllAsync()
        {
            return GetAsync<List<KbkNumberDto>>("/GetAll", null);
        }

        public Task<List<KbkDto>> GetKbkNumberAndTypeByIdListAsync(IReadOnlyCollection<int> kbkIds)
        {
            return PostAsync<IReadOnlyCollection<int>, List<KbkDto>>("/GetKbkNumberAndTypeByIdList", kbkIds);
        }

        public Task<string> GetActualKbkForSpecialDateAsync(string typeCode, DateTime endDate)
        {
            return GetAsync<string>("/GetActualKbkForSpecialDate", new {typeCode, endDate});
        }

        public Task<KbkType> GetKbkTypeByNumberAsync(string kbkNumber)
        {
            return GetAsync<KbkType>("/GetKbkTypeByNumber", new {kbkNumber});
        }

        public Task<KbkNumberDto> GetKbkNumber(KbkNumberRequestDto request)
        {
            return PostAsync<KbkNumberRequestDto, KbkNumberDto>("/GetKbkNumber", request);
        }
        
        public Task<List<KbkNumberDto>> GetKbkNumbersAsync(KbkNumbersRequestDto request)
        {
            return PostAsync<KbkNumbersRequestDto, List<KbkNumberDto>>("/GetKbkNumbers", request);
        }

        public Task<List<KbkDto>> GetKbkByNumbersAsync(IReadOnlyCollection<string> kbkNumbers)
        {
            return PostAsync<IReadOnlyCollection<string>, List<KbkDto>>("/GetKbkByNumbers", kbkNumbers);
        }

        public Task<List<KbkNumberDto>> GetKbkNumbersByTypesAsync(KbkNumberType kbkType, KbkPaymentType kbkPaymentType)
        {
            return GetAsync<List<KbkNumberDto>>("/GetKbkNumbersByTypes", new { kbkType, kbkPaymentType });
        }
    }
}