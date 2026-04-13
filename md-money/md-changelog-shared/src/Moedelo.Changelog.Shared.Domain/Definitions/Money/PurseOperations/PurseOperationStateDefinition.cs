namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations
{
    public abstract class PurseOperationStateDefinition<TDefinition, TState>
        : MoneyStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected PurseOperationStateDefinition()
        {
            AddTag("purse_operation");
        }
    }
}
