using System;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Tariff;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;

namespace Moedelo.CatalogV2.Client.Tariff
{
    public interface ITariffApiClient : IDI
    {
        [Obsolete("Use Moedelo.Tariffs.Client.Tariffs.ITariffsClient")]
        Task<List<TariffDto>> GetAllAsync();

        Task<string> GetTariffDataAsync(int id);
    }
}