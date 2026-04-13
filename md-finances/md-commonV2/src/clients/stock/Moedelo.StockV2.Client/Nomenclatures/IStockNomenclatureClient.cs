using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.StockV2.Dto.Nomenclatures;

namespace Moedelo.StockV2.Client.Nomenclatures
{
    public interface IStockNomenclatureClient : IDI
    {
        Task<List<StockNomenclatureDto>> GetAllAsync(int firmId, int userId);
        Task CreateDefaultsAsync(int firmId, int userId);
        Task<long?> Save(int firmId, int userId, StockNomenclatureDto nomenclature);
    }
}