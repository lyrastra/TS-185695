using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PriceLists.Dto.PriceLists;

namespace Moedelo.PriceLists.Client.PriceLists
{
    /// <summary> Клиент для управления прайс-листами </summary>
    public interface IPriceListsClient : IDI
    {
        /// <summary>
        /// Получить прайс-лист по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Прайс-лист</returns>
        Task<PriceListDto> GetByIdAsync(int id);

        /// <summary>
        /// Получить список всех прайс-листов
        /// </summary>
        /// <returns>Список прайс-листов</returns>
        Task<List<PriceListDto>> GetAllAsync();
        
        /// <summary>
        /// Получить список всех действующих прайс-листов
        /// </summary>
        /// <returns>Список прайс-листов</returns>
        Task<List<PriceListDto>> GetActualAsync();

        /// <summary>
        /// Получить список прайс-листов по идентификатору тарифа
        /// </summary>
        /// <param name="tariffId">Идентификатор тарифа</param>
        /// <returns>Список прайс-листов</returns>
        Task<List<PriceListDto>> GetByTariffIdAsync(int tariffId);

        /// <summary>
        /// Получить список прайс-листов по списку идентификаторов
        /// </summary>
        Task<List<PriceListDto>> GetByIdListAsync(IReadOnlyCollection<int> priceListIds);

        /// <summary>
        /// Получить список прайс-листов по идентификаторам тарифа
        /// </summary>
        /// <param name="tariffIds">Идентификаторы тарифов</param>
        /// <returns>Список прайс-листов</returns>
        Task<List<PriceListDto>> GetByTariffIdsAsync(List<int> tariffIds);

        /// <summary>
        /// Получить список доступных прайс-листов для партнера
        /// </summary>
        /// <returns>Список прайс-листов</returns>
        Task<List<PriceListDto>> GetListForPartnerAsync();
        
        /// <summary>
        /// Получить список прайс-листов с IsOpenForPartnerRegistration = 1
        /// </summary>
        /// <returns>Список прайс-листов</returns>
        Task<List<PriceListDto>> GetAvailableForPartnerRegistrationAsync();

        /// <summary>
        /// Получить список прайс-листов по шаблону имени
        /// </summary>
        /// <param name="namePattern">Шаблон имени</param>
        /// <param name="offset">Смещение</param>
        /// <param name="size">Количество</param>
        /// <returns>Список прайс-листов</returns>
        Task<List<PriceListDto>> GetPagedListByNamePatternAsync(string namePattern, int offset, int size);

        /// <summary>
        /// Создать прайс-лист
        /// </summary>
        /// <param name="model">Прайс-лист</param>
        /// <returns>Идентификатор созданного прайс-листа</returns>
        Task<int> CreateAsync(PriceListDto model);

        /// <summary>
        /// Обновить прайс-лист
        /// </summary>
        /// <param name="model">Прайс-лист</param>
        /// <returns></returns>
        Task UpdateAsync(PriceListDto model);

        /// <summary>
        /// Удалить прайс-лист, если не используется в платежах
        /// </summary>
        /// <param name="model">Параметр для удаления прайс-листа</param>
        /// <returns>Удален ли прайс-лист</returns>
        Task<bool> DeleteIfNotUsingAsync(DeletePriceListDto model);
        
        /// <summary>
        /// Получить позиции прайс-листа
        /// </summary>
        /// <param name="priceListId">Id прайс-листа</param>
        /// <returns>Список позиций прайс-листа</returns>
        Task<List<PriceListPositionDto>> GetPriceListPositionsAsync(int priceListId);
    }
}