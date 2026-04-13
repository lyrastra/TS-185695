using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext
{
    public interface IAuthorizationContext
    {
        int FirmId { get; }

        int UserId { get; }

        /// <summary>
        /// Авторизован ли пользователь в фирме
        /// </summary>
        bool IsAuthorized { get; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        Task<int> GetRoleIdAsync();

        /// <summary>
        /// Есть ли право
        /// </summary>
        Task<bool> HasRuleAsync(AccessRule accessRule);

        /// <summary>
        /// Есть ли все права из списка
        /// </summary>
        Task<bool> HasAllRuleAsync(params AccessRule[] accessRules);

        /// <summary>
        /// Есть ли хотя бы одно право из списка
        /// </summary>
        Task<bool> HasAnyRuleAsync(params AccessRule[] accessRules);

        /// <summary>
        /// Есть ли хотя бы одно право из списка в тарифе
        /// </summary>
        Task<bool> HasAnyRuleInTariffAsync(params AccessRule[] accessRules);

        /// <summary>
        /// возвращает из указанного списка прав подсписок прав, присутствующих у пользователя в данной фирме
        /// </summary>
        /// <param name="rulesToCheck"></param>
        /// <returns></returns>
        Task<List<AccessRule>> GetGrantedRulesAsync(params AccessRule[] rulesToCheck);

        /// <summary>
        /// Перезагрузить данные при следующем обращении к любому из полей
        /// В целом, нет особого смысла в использовании данного метода, потому что оперативность пересчёта прав
        /// в общем случае неизвестна
        /// </summary>
        void Invalidate();

        /// <summary>
        /// все права, доступные пользователю в рамках исполняемой им роли в данной фирме
        /// </summary>
        /// <returns>список доступных пользователю прав</returns>
        Task<IReadOnlyCollection<AccessRule>> GetUserRulesAsync();
    }
}
