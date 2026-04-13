using System.Collections.Generic;

namespace Moedelo.Accounts.Kafka.Abstractions.Events.Users
{
    /// <summary>
    /// Событие "У пользователя сменились права в бэк-офисе (партнёрке)"
    /// </summary>
    public abstract class UserBackofficePermissionsChanged
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// набор прав пользователея (после изменений)
        /// значения см. github.com/moedelo/md-enums/blob/master/src/common/Moedelo.Common.Enums/Enums/Access/AccessRule.cs
        /// версия для core https://github.com/moedelo/md-common-accessrules/blob/master/src/Moedelo.Common.AccessRules.Abstractions/AccessRule.cs
        /// </summary>
        public IReadOnlyCollection<int> Permissions { get; set; }
    }
}