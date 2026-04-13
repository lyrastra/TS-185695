using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Integrations
{
    public interface IIntegrationErrorsService : IDI
    {
        Task<List<IntegrationErrorResponse>> GetIntegrationErrorsAsync(int firmId, CancellationToken ctx);

        Task SetIntegrationErrorAsReadAsync(int firmId, IReadOnlyCollection<int> errorIds);
    }
}