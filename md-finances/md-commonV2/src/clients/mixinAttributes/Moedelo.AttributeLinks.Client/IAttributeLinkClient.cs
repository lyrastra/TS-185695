using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AttributeLinks.Client.Internals;
using Moedelo.Attributes.Client;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AttributeLinks.Client
{
    /// <summary> Клиент для работы с линками атрибутов объектов</summary>
    public interface IAttributeLinkClient : IDI
    {
        /// <summary>
        /// Получить кеш по типу.
        /// В кеш вычитываются все данные по данному типу.
        /// Данных может быть ОЧЕНЬ много поэтому стоит ограничение на стороне сервера.
        /// Если кешировать нельзя возвращается null
        /// Возможность кеширования типов является статической и устанавливается
        /// на этапе разработки и динамически не меняется.
        /// </summary>
        /// <returns>Кеш</returns>
        Task<CacheAttributeLink> GetCacheAttributeLinkAsync(AttributeObjectType attributeObjectType);

        /// <summary>
        /// Проверка кеширования линков
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<bool> IsCachedAsync(AttributeObjectType type);
        
        /// <summary>
        /// Получить список линков атрибутов без описания по типу атрибута.
        /// Используется для обновления кэша
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<List<AttributeLink>> GetListForCacheAsync(AttributeObjectType type);
        
        /// <summary>
        /// Получить список связей по списку обьектов 
        /// </summary>
        /// <returns>Список атрибутов объекта</returns>
        Task<List<AttributeLink>> GetListByObjectsAsync(AttributeObjectType attributeObjectType, IEnumerable<string> objectId);

        /// <summary>
        /// Получение всех связей для аттрибутов
        /// </summary>
        /// <returns></returns>
        Task<List<AttributeLink>> GetListByAttributesAsync(AttributeObjectType attributeObjectType, IEnumerable<string> names);

        /// <summary>
        /// Записать список связей. Данные созадются или обновляется если уже есть
        /// </summary>
        Task SetAsync(params AttributeLink[] link);

        /// <summary>
        /// Записать список связей. Данные созадются или обновляется если уже есть
        /// </summary>
        Task SetAsync(IEnumerable<AttributeLink> links);

        /// <summary>
        /// Удалить список связей.
        /// </summary>
        Task DeleteAsync(params AttributeLink[] link);

        /// <summary>
        /// Удалить список связей.
        /// </summary>
        Task DeleteAsync(IEnumerable<AttributeLink> links);
    }
}