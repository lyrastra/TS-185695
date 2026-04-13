using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader
{
    public interface IBankIntegrationDataReader : IDI
    {
        Task<Dictionary<long, IntegrationData>> GetBankIntegrationDataAsync(int firmId, int userId, IReadOnlyCollection<MoneySource> sources, Dictionary<int, SourceBankData> banks);
    }
}
