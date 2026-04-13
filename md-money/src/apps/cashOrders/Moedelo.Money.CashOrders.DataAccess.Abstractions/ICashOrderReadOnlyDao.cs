using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.CashOrders.DataAccess.Abstractions
{
    public interface ICashOrderReadOnlyDao
    {
        Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}