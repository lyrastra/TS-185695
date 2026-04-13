using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/8e722e57b9bfb9563081b621b5bb88b16f59f0a7/src/clients/stock/Moedelo.StockV2.Client/Nomenclatures/StockNomenclatureClient.cs
    /// </summary>
    public interface IStockNomenclatureApiClient
    {
        Task<long?> SaveAsync(FirmId firmId, UserId userId, StockNomenclatureDto nomenclature);

        Task CreateDefaultsAsync(FirmId firmId, UserId userId);

        Task<List<StockNomenclatureDto>> GetAllAsync(FirmId firmId, UserId userId);
    }
}