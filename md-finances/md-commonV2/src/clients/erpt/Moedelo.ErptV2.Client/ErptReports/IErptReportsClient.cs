using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.ErptReports
{
    public interface IErptReportsClient : IDI
    {
        Task<byte[]> GetEdsProblemReport();
    }
}