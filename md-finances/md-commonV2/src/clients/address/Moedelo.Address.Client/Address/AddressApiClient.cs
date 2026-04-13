using System.Collections.Generic;
using System.Threading;
using Moedelo.Address.Dto.Address;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Address.Client.Address
{
#pragma warning disable CS0618 // Type or member is obsolete
    [InjectAsSingleton(typeof(IAddressApiClient))]
    public class AddressApiClient : BaseApiClient, IAddressApiClient
    {
#pragma warning restore CS0618 // Type or member is obsolete
        private readonly SettingValue apiEndPoint;
        
        public AddressApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("AddressApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<string> GetFirmAddressString(int firmId, bool withAdditionalInfo)
        {
            return GetAsync<string>("/GetFirmAddressString", new { firmId, withAdditionalInfo });
        }

        public Task<AddressDto> GetFirmAddress(int firmId)
        {
            return GetAsync<AddressDto>("/GetFirmAddress", new { firmId });
        }

        public Task<AddressV2Dto> GetFirmV2AddressAsync(int firmId, CancellationToken ctx)
        {
            return GetAsync<AddressV2Dto>("/FirmAddress/GetAddress", new { firmId }, cancellationToken: ctx);
        }

        public Task<List<string>> GetFirmAddressStringList(List<int> firmIds, bool withAdditionalInfo)
        {
            var dto = new GetFirmAddressStringListDto { FirmIds = firmIds, WithAdditionalInfo = withAdditionalInfo };
            return PostAsync<GetFirmAddressStringListDto, List<string>>("/GetFirmAddressStringList", dto);
        }

        public Task<List<AddressDto>> GetFirmAddressList(List<int> firmIds)
        {
            return PostAsync<List<int>, List<AddressDto>>("/GetFirmAddressList", firmIds);
        }

        public Task<List<AddressDto>> GetAddressListByCodesAsync(List<string> codes)
        {
            return PostAsync<List<string>, List<AddressDto>>("/GetAddressListByCodes", codes);
        }

        public Task SetFirmAddress(AddressSaveDto dto)
        {
            return PostAsync("/SaveFirmAddress", dto);
        }

        public Task<AddressDto> GetAddress(long id)
        {
            return GetAsync<AddressDto>("/GetAddress", new { id });
        }

        public Task<long> SetAddress(AddressSaveDto dto)
        {
            return PostAsync<AddressSaveDto, long>("/SaveAddress", dto);
        }

        public Task<string> GetAddressString(long id, bool withAdditionalInfo)
        {
            return GetAsync<string>("/GetAddressString", new { id, withAdditionalInfo });
        }

        public Task<List<string>> GetAddressStringList(List<long> ids, bool withAdditionalInfo)
        {
            var dto = new GetAddressStringListDto { AddressIds = ids, WithAdditionalInfo = withAdditionalInfo };
            return PostAsync<GetAddressStringListDto, List<string>>("/GetAddressStringList", dto);
        }

        public Task<List<AddressDto>> GetAddressList(List<long> ids)
        {
            return PostAsync<List<long>, List<AddressDto>>("/GetAddressList", ids);
        }

        public Task<AddressObjectDto> GetAddressObject(string kladrCode)
        {
            return GetAsync<AddressObjectDto>("/GetAddressObject", new { kladrCode });
        }

        public Task DeleteFirmAddressAsync(int firmId)
        {
            return DeleteAsync("/DeleteFirmAddress", new { firmId });
        }

        public Task DeleteAddressAsync(long addressId, int firmId)
        {
            return DeleteAsync("/DeleteAddress", new { addressId, firmId });
        }
    }
}