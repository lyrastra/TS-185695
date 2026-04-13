using System;

namespace Moedelo.Docs.ApiClient.Abstractions.RelinkSources
{
    public interface IRelinkSourceDocument
    {
        long DocumentBaseId { get; }
        string Number { get; }
        DateTime Date { get; }
        decimal Sum { get; }
        int KontragentId { get; }
    }
}