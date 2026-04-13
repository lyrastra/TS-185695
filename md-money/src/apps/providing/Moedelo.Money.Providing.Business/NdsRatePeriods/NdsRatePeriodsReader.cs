using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.NdsRatePeriods.Models;
using Moedelo.Requisites.ApiClient.Abstractions.Clients;
using Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto;

namespace Moedelo.Money.Providing.Business.NdsRatePeriods;

[InjectAsSingleton(typeof(NdsRatePeriodsReader))]
class NdsRatePeriodsReader
{
    private readonly INdsRatePeriodsApiClient ndsRatePeriodsApiClient;

    public NdsRatePeriodsReader(INdsRatePeriodsApiClient ndsRatePeriodsApiClient)
    {
        this.ndsRatePeriodsApiClient = ndsRatePeriodsApiClient;
    }

    public async Task<IReadOnlyList<NdsRatePeriod>> GetAsync()
    {
        var response = await ndsRatePeriodsApiClient.GetAsync(new GetNdsRatePeriodsFilterDto { });
        return response.Select(Map).ToArray();
    }

    private static NdsRatePeriod Map(NdsRatePeriodDto dto)
    {
        return new NdsRatePeriod
        {
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Rate = dto.Rate
        };
    }
}