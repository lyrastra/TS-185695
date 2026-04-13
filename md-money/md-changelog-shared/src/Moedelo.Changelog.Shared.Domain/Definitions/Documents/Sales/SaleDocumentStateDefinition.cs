namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents.Sales
{
    public abstract class SaleDocumentStateDefinition<TDefinition, TState>
        : DocumentStateDefinition<TDefinition, TState>
        where TDefinition : SaleDocumentStateDefinition<TDefinition, TState>, new()
    {
        protected SaleDocumentStateDefinition()
        {
            AddTag("sales_document");
        }
    }
}
