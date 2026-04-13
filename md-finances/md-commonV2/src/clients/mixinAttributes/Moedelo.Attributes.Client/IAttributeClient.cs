using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Attributes.Client.Internals;
using Moedelo.Common.Enums.Enums.mixinAttributes;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Attributes.Client
{
    /// <summary> Клиент для работы с атрибутами</summary>
    public interface IAttributeClient : IDI
    {
        /// <summary>
        /// Получить список атрибутов по типу атрибута
        /// </summary>
        /// <param name="attributeObjectType">Тип атрибута</param>
        /// <returns>Список атрибутов</returns>
        CacheAttributeObject GetCacheAttributeObject(AttributeObjectType attributeObjectType);
        
        /// <summary>
        /// Получить список атрибутов без описания по типу атрибута.
        /// Используется для обновления кэша
        /// </summary>
        /// <param name="attributeObjectType">Тип атрибута</param>
        /// <returns>Список атрибутов</returns>
        Task<List<AttributeObject>> GetListForCacheAsync(AttributeObjectType attributeObjectType);
        
        /// <summary>
        /// Получить список атрибутов по типу атрибута
        /// </summary>
        /// <param name="attributeObjectType">Тип атрибута</param>
        /// <returns>Список атрибутов</returns>
        Task<List<FullAttributeObject>> GetListAsync(AttributeObjectType attributeObjectType);
        
        /// <summary>
        /// Получить атрибут по типу атрибута и имени
        /// </summary>
        /// <param name="attributeObjectType">Тип атрибута</param>
        /// <returns>Список атрибутов</returns>
        Task<FullAttributeObject> GetAsync(AttributeObjectType attributeObjectType, string name);
        
        /// <summary>
        /// Создать / обновить атрибут
        /// </summary>
        /// <param name="dto">Атрибут</param>
        /// <returns></returns>
        Task SetAsync(AttributeObjectType attributeObjectType, string name, string description);

        /// <summary>
        /// Удалить атрибуты
        /// </summary>
        /// <param name="ids">Список идентификаторов атрибутов</param>
        /// <returns></returns>
        Task DeleteAsync(AttributeObjectType attributeObjectType, IEnumerable<string> names);
        /// <summary>
        /// Удалить атрибуты
        /// </summary>
        /// <param name="ids">Список идентификаторов атрибутов</param>
        /// <returns></returns>
        Task DeleteAsync(AttributeObjectType attributeObjectType, params string[] names);
    }
}