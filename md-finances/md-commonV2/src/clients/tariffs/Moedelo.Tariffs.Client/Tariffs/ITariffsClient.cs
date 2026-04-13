using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Tariffs.Dto.Tariffs;

namespace Moedelo.Tariffs.Client.Tariffs
{
    /// <summary> Клиент для тарифов </summary>
    public interface ITariffsClient : IDI
    {
        /// <summary>
        /// Получить тариф по идентификатору
        /// </summary>
        Task<TariffDto> GetByIdAsync(int id);
        
        /// <summary>
        /// Получить список тарифов по идентификаторам
        /// </summary>
        Task<List<TariffDto>> GetAsync(IReadOnlyCollection<int> tariffIds);

        /// <summary>
        /// Получить тарифы по списку идентификаторов прайс листов
        /// </summary>
        [Obsolete("Use GetByPriceListIdsAsync instead")]
        Task<List<TariffDto>> GetByPriceListIdListAsync(IReadOnlyCollection<int> priceListIdList);
        
        /// <summary>
        /// Получить тарифы по списку идентификаторов прайс листов
        /// </summary>
        Task<IReadOnlyDictionary<int, TariffDto>> GetByPriceListIdsAsync(IReadOnlyCollection<int> priceListIds);

        /// <summary>
        /// Получить список всех тарифов
        /// </summary>
        /// <returns>Список тарифов</returns>
        Task<List<TariffDto>> GetAllAsync();

        /// <summary>
        /// Получить список тарифов по критериям
        /// </summary>
        /// <param name="namePattern">Шаблон имени</param>
        /// <param name="isWithAccess">С доступом к сервису</param>
        /// <param name="isOneTime">Разовая услуга</param>
        /// <param name="offset">Смещение</param>
        /// <param name="size">Количество</param>
        /// <returns>Список тарифов</returns>
        Task<List<TariffDto>> GetPagedListByCriteriaAsync(string namePattern, bool? isWithAccess, bool? isOneTime, int offset, int size);

        /// <summary>
        /// Какая-то старая проверка специфического свойства тарифа, которое используется в некоторых логиках в разных доменах
        /// </summary>
        /// <param name="tariffId">идентификатор тарифа, для которого проверяется свойство</param>
        /// <param name="paymentDate">дата, на которую проверяется значение свойства</param>
        /// <returns></returns>
        Task<bool> IsLegacyLicenseActOnlyTariffAsync(int tariffId, DateTime paymentDate);

        /// <summary>
        /// Получить список тарифов, удовлетворяющих заданным условиям
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns>список тарифов</returns>
        Task<List<TariffDto>> GetByAsync(TariffFilterDto requestDto);
    }
}
