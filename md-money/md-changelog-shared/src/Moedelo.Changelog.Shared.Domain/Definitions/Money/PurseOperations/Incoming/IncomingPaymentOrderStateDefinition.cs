namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Incoming
{
    public abstract class IncomingPurseOperationStateDefinition<TDefinition, TState>
        : PurseOperationStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected IncomingPurseOperationStateDefinition()
        {
            AddTag("incoming_purse_operation");
        }
    }
}
