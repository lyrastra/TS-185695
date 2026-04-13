using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccPostings.Client
{
    public interface ISubcontoMergeClient : IDI
    {
        Task ReplaceRelationsAsync(int firmId, int userId, SubcontoMergeDto request);
    }
}