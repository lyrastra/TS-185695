using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/e5b042aae023884391f53de94c6f2724b96cd1b6/src/clients/estate/Moedelo.Estate.Client/InventoryCard/IInventoryCardClient.cs#L8
    /// </summary>
    public interface IInventoryCardApiClient
    {
        Task<List<InventoryCardDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task<PaidSumDto[]> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает ИК, связанные с первичными документами в ОС
        /// Результат сгруппирован по base id первичного документа
        /// </summary>
        Task<Dictionary<long, InventoryCardDto>> GetByPrimaryDocumentBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> primaryDocumentBaseIds);
        
        /// <summary>
        /// Провести в НУ ИК, связанные с ВА (передается список BaseId ВА). Актуально для УСН "Доходы - расходы"
        /// </summary>
        Task TaxProvideByFixedAssetInvestmentBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> fixedAssetBaseIds);
    }
}
