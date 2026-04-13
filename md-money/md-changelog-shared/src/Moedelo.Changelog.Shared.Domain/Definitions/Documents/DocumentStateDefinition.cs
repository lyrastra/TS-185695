namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public abstract class DocumentStateDefinition<TDefinition, TState>
        : AutoEntityStateDefinition<TDefinition, TState>
        where TDefinition : DocumentStateDefinition<TDefinition, TState>, new()
    {
        protected DocumentStateDefinition()
        {
            AddTag("document");
        }
    }
}
