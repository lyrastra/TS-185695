using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OfficeV2.Dto.Egr;
using Moedelo.OfficeV2.Dto.Egr.IpModel;
using Moedelo.OfficeV2.Dto.Egr.Search;
using Moedelo.OfficeV2.Dto.Egr.UlModel;

namespace Moedelo.OfficeV2.Client.FnsExcerpt
{
  [InjectAsSingleton]
  public class EgrExcerptApiClient : BaseApiClient, IEgrExcerptApiClient
  {
    private readonly SettingValue apiEndpoint;
    
    public EgrExcerptApiClient(
      IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository) : base(
        httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
    {
        apiEndpoint = settingRepository.Get("OfficePrivateApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }

    public async Task<EgrUlResponceDto> GetUlExcerptAsync(GetExcerptByInnAndOgrnRequestDto request)
    {
        var settings = new HttpQuerySetting(new TimeSpan(0, 1, 40));
        return (await GetAsync<DataDto<EgrUlResponceDto>>(GetFullMethodName("GetUlExcerptByInnAndOgrn"), request, setting: settings).ConfigureAwait(false)).Data;
    }

    public async Task<EgrUlResponceDto> GetUlExcerptAsync(int id)
    {
      return (await GetAsync<DataDto<EgrUlResponceDto>>(GetFullMethodName("GetUlExcerptById"), new
      {
        id
      }).ConfigureAwait(false)).Data;
    }

    public async Task<EgrIpResponceDto> GetIpExcerptAsync(GetExcerptByInnAndOgrnRequestDto request)
    {
      var settings = new HttpQuerySetting(new TimeSpan(0, 1, 40));
      return (await GetAsync<DataDto<EgrIpResponceDto>>(GetFullMethodName("GetIpExcerptByInnAndOgrn"), request, setting: settings).ConfigureAwait(false)).Data;
    }

    public async Task<EgrIpResponceDto> GetIpExcerptAsync(int id)
    {
      return (await GetAsync<DataDto<EgrIpResponceDto>>(GetFullMethodName("GetIpExcerptById"), new
      {
        id
      }).ConfigureAwait(false)).Data;
    }

    public async Task<SearchEgrResponseDto> SearchAsync(SearchEgrRequestDto request)
    {
      return (await PostAsync<SearchEgrRequestDto, DataDto<SearchEgrResponseDto>>(GetFullMethodName(nameof (SearchAsync)), request).ConfigureAwait(false)).Data;
    }

    public async Task<SearchEgrResponseDto> SearchDebugAsync(SearchEgrRequestDto request)
    {
      return (await PostAsync<SearchEgrRequestDto, DataDto<SearchEgrResponseDto>>(GetFullMethodName(nameof (SearchDebugAsync)), request).ConfigureAwait(false)).Data;
    }

    public async Task<List<QueryStatResponseDto>> GetQueryStatAsync(QueryStatRequestDto request)
    {
      return (await PostAsync<QueryStatRequestDto, ListDto<QueryStatResponseDto>>(GetFullMethodName(nameof (GetQueryStatAsync)), request).ConfigureAwait(false)).Items.ToList();
    }

    private static string GetFullMethodName(string method)
    {
      return "/Rest/EgrExcerpt/" + method;
    }
  }
}