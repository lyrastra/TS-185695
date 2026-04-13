using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Address.ApiClient.Abstractions;
using Moedelo.Address.ApiClient.Abstractions.legacy;
using Moedelo.Address.ApiClient.Abstractions.legacy.Dto.FiasFormatAutocomplete;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Address.ApiClient.NetFramework
{
    [InjectAsSingleton(typeof(IAddressSuggestionFiasFormatClient))]
    public class AddressSuggestionFiasFormatClient : BaseCoreApiClient, IAddressSuggestionFiasFormatClient
    {
        private const string Tag = nameof(AddressSuggestionFiasFormatClient);
        private readonly SettingValue apiEndPoint;
        private const string Prefix = "/private/api/v1/FiasFormat";
        private readonly SettingValue isGarAvailable;

        private readonly IAddressAutocompleteV2ApiClient addressAutocompleteV2ApiClient;
        private readonly ILogger logger;

        public AddressSuggestionFiasFormatClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            IAddressAutocompleteV2ApiClient addressAutocompleteV2ApiClient,
            ILogger logger)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.addressAutocompleteV2ApiClient = addressAutocompleteV2ApiClient;
            this.logger = logger;
            apiEndPoint = settingRepository.Get("AddressSuggestionApiEndpoint");
            isGarAvailable = settingRepository.Get("IsGarAvailable");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + Prefix;
        }

        public async Task<List<LocationAutocompleteFiasFormatResponse>> GetLocationAutocompleteAsync(int firmId, int userId, string query)
        {
            if (isGarAvailable.GetBoolValue())
            {
                var request = new LocationAutocompleteFiasFormatRequest
                {
                    Query = query
                };

                var locationsFromGar = await GetGarLocationAutocompleteAsync(firmId, userId, request).ConfigureAwait(false);

                if (locationsFromGar?.Any() == false && string.IsNullOrEmpty(query) == false)
                {
                    var locationsFromFias = await GetFiasLocationAutocompleteAsync(query).ConfigureAwait(false);
                    if (locationsFromFias != null && locationsFromFias.Any())
                    {
                        logger.Info(Tag, $"Не удалось найти результат для поля 'Город или населенный пункт' в ГАР, но удалось в ФИАС", extraData: new { query, firmId, userId });
                    }
                }

                return locationsFromGar;
            }

            return await GetFiasLocationAutocompleteAsync(query).ConfigureAwait(false);
        }

        public async Task<List<StreetAutocompleteFiasFormatResponse>> GetStreetAutocompleteAsync(int firmId, int userId, Guid parentGuid, string query, string parentName)
        {
            if (isGarAvailable.GetBoolValue())
            {
                var streetsFromGar = await GetGarStreetAutocompleteAsync(firmId, userId, parentGuid, query, parentName).ConfigureAwait(false);

                if (streetsFromGar?.Any() == false && string.IsNullOrEmpty(query) == false)
                {
                    var locationsFromFias = await GetFiasStreetAutocompleteAsync(parentGuid, query, parentName).ConfigureAwait(false);
                    if (locationsFromFias != null && locationsFromFias.Any())
                    {
                        logger.Info(Tag, $"Не удалось найти результат для поля 'Улица' в ГАР, но удалось в ФИАС", extraData: new { parentGuid, query, parentName, firmId, userId });
                    }
                }

                return streetsFromGar;
            }

            return await GetFiasStreetAutocompleteAsync(parentGuid, query, parentName).ConfigureAwait(false);
        }

        public async Task<AddressParamsFiasFormatResponse> GetAddressParamsAsync(int firmId, int userId, AddressParamsFiasFormatRequest request)
        {
            if (isGarAvailable.GetBoolValue())
            {
                return await GetGarAddressParamsAsync(firmId, userId, request).ConfigureAwait(false);
            }

            return await GetFiasAddressParamsAsync(request).ConfigureAwait(false);
        }

        private async Task<List<LocationAutocompleteFiasFormatResponse>> GetGarLocationAutocompleteAsync(int firmId, int userId, LocationAutocompleteFiasFormatRequest request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PostAsync<object, ApiDataResult<List<LocationAutocompleteFiasFormatResponse>>>(
                    "/GetLocationAutocomplete",
                    request,
                    queryHeaders:tokenHeaders)
                .ConfigureAwait(false);

            return result.data;
        }

        private async Task<List<LocationAutocompleteFiasFormatResponse>> GetFiasLocationAutocompleteAsync(string query)
        {
            var result = await addressAutocompleteV2ApiClient.GetLocationAutocompleteAsync(query).ConfigureAwait(false);

            if (result == null || result.Any() == false)
            {
                return new List<LocationAutocompleteFiasFormatResponse>();
            }

            return result.Select(location => new LocationAutocompleteFiasFormatResponse
            {
                City = location.City,
                CityAoGuid = location.CityAoGuid,
                District = location.District,
                DistrictAoGuid = location.DistrictAoGuid,
                Region = location.Region,
                RegionAoGuid = location.RegionAoGuid,
                RegionCode = location.RegionCode,
                SubArea = location.SubArea,
                SubAreaAoGuid = location.SubAreaAoGuid,
                PlanningStructure = location.PlanningStructure,
                PlanningStructureAoGuid = location.PlanningStructureAoGuid,
                Locality = location.Locality,
                LocalityAoGuid = location.LocalityAoGuid
            }).ToList();
        }

        private async Task<List<StreetAutocompleteFiasFormatResponse>> GetGarStreetAutocompleteAsync(int firmId, int userId, Guid parentGuid, string query, string parentName)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var request = new StreetAutocompleteFiasFormatRequest
            {
                ParentName = parentName,
                ParentGuid = parentGuid,
                Query = query
            };

            var result = await PostAsync<StreetAutocompleteFiasFormatRequest, ApiDataResult<List<StreetAutocompleteFiasFormatResponse>>>(
                    "/GetStreetAutocomplete",
                    request,
                    queryHeaders:tokenHeaders)
                .ConfigureAwait(false);

            return result.data;
        }

        private async Task<List<StreetAutocompleteFiasFormatResponse>> GetFiasStreetAutocompleteAsync(Guid parentGuid, string query, string parentName)
        {
            var streetList = await addressAutocompleteV2ApiClient.GetStreetAutocompleteAsync(parentGuid, query, parentName).ConfigureAwait(false);
            if (streetList == null || streetList.Any() == false)
            {
                return new List<StreetAutocompleteFiasFormatResponse>();
            }

            return streetList.Select(street => new StreetAutocompleteFiasFormatResponse
            {
                Guid = street.Guid,
                FullName = street.FullName
            }).ToList();
        }

        private async Task<AddressParamsFiasFormatResponse> GetGarAddressParamsAsync(int firmId, int userId,
            AddressParamsFiasFormatRequest request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PostAsync<AddressParamsFiasFormatRequest, ApiDataResult<AddressParamsFiasFormatResponse>>(
                    "/GetAddressParams",
                    request,
                    queryHeaders:tokenHeaders)
                .ConfigureAwait(false);

            return result.data;
        }

        private async Task<AddressParamsFiasFormatResponse> GetFiasAddressParamsAsync(AddressParamsFiasFormatRequest request)
        {
            var result = await addressAutocompleteV2ApiClient.GetAddressParamsAsync(
                request.IsOoo,
                request.Guid,
                request.House,
                request.Building,
                request.BuildingName
            ).ConfigureAwait(false);

            return result == null
                ? new AddressParamsFiasFormatResponse()
                : new AddressParamsFiasFormatResponse
                {
                    Okato = result.Okato,
                    Oktmo = result.Oktmo,
                    PostIndex = result.PostIndex,
                    FnsCode = result.FnsCode
                };
        }
    }
}
