namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents.Purchases
{
    public abstract class PurchaseDocumentStateDefinition<TDefinition, TState>
        : DocumentStateDefinition<TDefinition, TState>
        where TDefinition : PurchaseDocumentStateDefinition<TDefinition, TState>, new()
    {
        protected PurchaseDocumentStateDefinition()
        {
            AddTag("purchase_document");
        }
    }
}
