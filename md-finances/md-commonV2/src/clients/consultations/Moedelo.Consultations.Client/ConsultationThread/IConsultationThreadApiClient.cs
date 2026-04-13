using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Consultations.Client.ConsultationThread
{
    public interface IConsultationThreadApiClient : IDI
    {
        Task<bool> HasAnyThreadAsync(int firmId);
    }
}