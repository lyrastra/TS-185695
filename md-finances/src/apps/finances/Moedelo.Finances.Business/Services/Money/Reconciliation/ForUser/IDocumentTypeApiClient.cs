using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Parsers.Klto1CParser.Enums;
using Moedelo.Parsers.Klto1CParser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Reconciliation.ForUser
{
    public interface IDocumentTypeApiClient : IDI
    {
        Task<IDictionary<string, TransferType>> DetermineAsync(int firmId, int userId, string settlementNumber, IReadOnlyCollection<Document> documents);
    }
}
