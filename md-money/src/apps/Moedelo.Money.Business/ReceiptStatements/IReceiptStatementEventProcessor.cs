using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.ReceiptStatements
{
    public interface IReceiptStatementEventProcessor
    {
        Task ProcessAsync(IReadOnlyCollection<long> createdLinkIds, IReadOnlyCollection<long> deletedLinkIds);
    }
}
