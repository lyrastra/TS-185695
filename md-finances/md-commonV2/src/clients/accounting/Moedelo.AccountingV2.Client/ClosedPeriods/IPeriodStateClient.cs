using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.ClosedPeriods
{
    public interface IPeriodStateClient: IDI
    {
        Task SaveQuarterAsync(int firmId, int userId, int year, int quarter);
    }
}
