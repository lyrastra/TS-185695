namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Outgoing
{
    public abstract class OutgoingPurseOperationStateDefinition<TDefinition, TState>
        : PurseOperationStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected OutgoingPurseOperationStateDefinition()
        {
            AddTag("outgoing_purse_operation");
        }
    }
}
