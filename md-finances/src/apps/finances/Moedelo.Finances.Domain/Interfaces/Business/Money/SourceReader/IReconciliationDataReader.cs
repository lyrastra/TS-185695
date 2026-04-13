using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader
{
    public interface IReconciliationDataReader: IDI
    {
        Task<Dictionary<long, ReconciliationData>> GetReconciliationDataAsync(int firmId, int userId, IReadOnlyCollection<MoneySource> sources);
    }
}
