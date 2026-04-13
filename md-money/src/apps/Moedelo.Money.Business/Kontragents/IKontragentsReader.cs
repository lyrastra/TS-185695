using Moedelo.Money.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Kontragents
{
    internal interface IKontragentsReader
    {
        Task<Kontragent> GetByIdAsync(int kontragentId);
        Task<IReadOnlyCollection<Kontragent>> GetByIdsAsync(IReadOnlyCollection<int> kontragentIds);
        Task<IReadOnlyCollection<KontragentWithRequisites>> GetWithRequisitesByIdsAsync(DateTime date, IReadOnlyCollection<int> kontragentIds);
    }
}
