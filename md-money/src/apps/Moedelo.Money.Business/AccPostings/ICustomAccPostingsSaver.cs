using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.AccPostings
{
    internal interface ICustomAccPostingsSaver
    {
        Task OverwriteAsync(long documentBaseId, OperationType operationType, IReadOnlyCollection<AccPosting> postings);
    }
}
