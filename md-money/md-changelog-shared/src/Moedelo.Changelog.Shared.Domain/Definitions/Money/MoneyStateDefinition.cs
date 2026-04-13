using System.Collections.Generic;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money
{
    public abstract class MoneyStateDefinition<TDefinition, TState>
        : AutoEntityStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected MoneyStateDefinition()
        {
            AddTag("money");
        }
        
        // кажется, этот набор прав подходит для чтения истории изменений всех (или большинства) типов операций в деньгах
        public override IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
            = new [] { AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank };

    }
}
