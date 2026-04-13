using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Outsouce;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IStockOutsourceApiClient
    {
        /// <summary>
        /// Получение остатков по складу массовой работы со складом в аутсорсе
        /// </summary>
        Task<OutsourceProductOnStockBalanceDto[]> GetProductsBalancesHistoryAsync(
            FirmId firmId, OutsourceProductsBlancesHistoryRequestDto requestDto);

        /// <summary>
        /// Получение данных по товарам для аутсорса
        /// </summary>
        Task<OutsourceProductInfoDto[]> GetProductsInfoAsync(FirmId firmId, IReadOnlyCollection<long> productIds);

        /// <summary>
        /// Получение списка товаров для аутсорса
        /// </summary>
        Task<OutsourceProductDto[]> GetProductsAsync(FirmId firmId);

        /// <summary>
        /// Получение данных по складам для аутсорса
        /// </summary>
        Task<OutsourceStockInfoDto[]> GetStocksInfoAsync(FirmId firmId, IReadOnlyCollection<long> stockIds);

        /// <summary>
        /// Получение списка синонимов по названию товара для аутсорса
        /// </summary>
        Task<OutsourceGetProductSynonymsResponseDto> GetProductSynonymsAsync(
            OutsourceGetProductSynonymsRequestDto requestDto);
    }
}