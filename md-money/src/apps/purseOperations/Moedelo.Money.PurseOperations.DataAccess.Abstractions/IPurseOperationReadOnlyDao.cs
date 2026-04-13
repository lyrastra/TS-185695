using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.PurseOperations.DataAccess.Abstractions
{
    public interface IPurseOperationReadOnlyDao
    {
        Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}