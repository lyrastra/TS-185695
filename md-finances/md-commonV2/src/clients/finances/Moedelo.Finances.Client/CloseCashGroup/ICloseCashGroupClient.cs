using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Client.CloseCashGroup
{
    public interface ICloseCashGroupClient : IDI
    {
        Task<DateTime?> GetLastClosedCashGroupDateAsync(int firmId, int userId);
    }
}
