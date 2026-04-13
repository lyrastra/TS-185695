using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.SelfCost;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface ITaxUnaccountedSelfCostApiClient
    {
        /// <summary>
        /// Сохраняет неучтенные в НУ суммы себестоимостей складских операциях (для перехода со средней на ФИФО)
        /// </summary>
        Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<SelfCostTaxUnaccountedSaveRequestDto> requests);

        /// <summary>
        /// Возвращает неучтенные в НУ суммы себестоимостей складских операциях (для перехода со средней на ФИФО)
        /// </summary>
        Task<List<SelfCostTaxUnaccountedDto>> GetAsync(FirmId firmId, UserId userId);
    }
}