using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Domain.Models.Kontragents;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations
{
    public interface IOperationDao : IDI
    {
        Task<MoneyOperation> GetByBaseIdAsync(int firmId, long documentBaseId);

        Task<MoneyOperation[]> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);

        Task<MoneyOperation[]> GetByPeriodAsync(int firmId, Period period);

        Task<MoneyOperation[]> GetByPatentAsync(int firmId, long patentId);

        Task<OperationKind?> GetKindByBaseIdAsync(int firmId, long documentBaseId);

        Task<List<OperationKindAndBaseId>> GetKindsByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds);

        Task<List<KontragentTurnover>> TopByOperationsWithKontragentsAsync(int userContextFirmId, int count, DateTime startDate, DateTime endDate);

        Task<List<OperationKindAndBaseId>> GetOperationsKindsByIdsAsync(int firmId, IReadOnlyCollection<long> ids);
    }
}
