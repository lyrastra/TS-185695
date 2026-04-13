using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.PfrEdmStatement
{
    public interface IPfrEdmStatementClient : IDI
    {
        Task<PfrEdmStatementStatus> GetStatusAsync(int firmId, int userId);
        Task ResetStatusAsync(int firmId);
    }
}