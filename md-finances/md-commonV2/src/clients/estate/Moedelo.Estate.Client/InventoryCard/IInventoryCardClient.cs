using Moedelo.Estate.Client.InventoryCard.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Estate.Client.InventoryCard
{
    public interface IInventoryCardClient : IDI
    {
        /// <summary>
        /// Возвращает ИК по списку DocumentBaseId
        /// </summary>
        Task<List<InventoryCardDto>> GetByDocumentBaseIds(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// Возвращает ИК по id
        /// </summary>
        Task<InventoryCardDto> GetById(int firmId, int userId, long id);

        /// <summary>
        /// Возвращает ИК по списку DocumentBaseId вложений во внеоборотные активы (ВА)
        /// </summary>
        Task<InventoryCardDto> GetByFixedAssetBaseIdAsync(int firmId, int userId, long documentBaseId);
        
        /// <summary>
        /// Обновляет ИК при сохранении остатков (проставляет стоимость в БУ)
        /// </summary>
        Task UpdateFromBalancesAsync(int firmId, int userId, IReadOnlyCollection<InventoryCardFromBalancesSaveDto> balances);
        
        /// <summary>
        /// Возвращает ИК, для которых начисляется амортизациия (МЗМ)
        /// </summary>
        Task<List<InventoryCardDto>> GetAmortizableInventoryCardsAsync(int firmId, int userId);
        
        /// <summary> 
        /// Сумма неоплаченного остатка по контрагенту (вводится в остатках)
        /// </summary>
        Task<InventoryCardNotPayedInBalancesDto> GetNotPayedInBalancesAsync(int firmId, int userId, int kontragentId, long baseId);

        /// <summary>
        /// Провести в НУ ИК, связанные с ВА (передается список BaseId ВА). Актуально для УСН "Доходы - расходы"
        /// </summary>
        Task TaxProvideByFixedAssetInvestmentBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> fixedAssetBaseIds);
        
        /// <summary>
        /// Обновить значение амортизации БУ (МЗМ)
        /// </summary>
        Task SaveAccAmortizationAsync(int firmId, int userId, IReadOnlyCollection<AccAmortizationUpdateRequestDto> saveRequest);
        
        /// <summary>
        /// Обновить значение амортизации НУ (МЗМ)
        /// </summary>
        Task SaveTaxAmortizationAsync(int firmId, int userId, IReadOnlyCollection<TaxAmortizationUpdateRequestDto> saveRequest);

        /// <summary>
        /// Создать ИК в остатках (для консоли перевода из БИЗ в УС)
        /// </summary>
        Task<InventoryCardFromBizToAccTransferResponseDto> CreateInventoryCardFromBizToAccTransferAsync(int firmId, int userId, InventoryCardFromBizToAccTransferSaveDto saveRequest);
        
        /// <summary>
        /// Возвращает ИК в виде словаря { BaseId_документа: ИК } по документам-вложениям
        /// </summary>
        Task<Dictionary<long, InventoryCardDto>> GetByPrimaryDocumentBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}
