using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.SelfCost;

namespace Moedelo.StockV2.Client.Operations
{
    public interface ITaxUnaccountedSelfCostApiClient : IDI
    {
        /// <summary>
        /// Сохраняет неучтенные в НУ суммы себестоимостей складских операциях (для перехода со средней на ФИФО)
        /// </summary>
        Task SaveAsync(int firmId, int userId, IReadOnlyCollection<SelfCostTaxUnaccountedSaveRequestDto> requests);

        /// <summary>
        /// Возвращает неучтенные в НУ суммы себестоимостей складских операциях (для перехода со средней на ФИФО)
        /// </summary>
        Task<List<SelfCostTaxUnaccountedDto>> GetAsync(int firmId, int userId);
    }
}