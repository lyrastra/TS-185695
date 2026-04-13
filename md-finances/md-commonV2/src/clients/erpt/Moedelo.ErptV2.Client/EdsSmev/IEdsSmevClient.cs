using Moedelo.ErptV2.Dto.EdsSmev;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto;

namespace Moedelo.ErptV2.Client.EdsSmev
{
    public interface IEdsSmevClient : IDI
    {
        Task<PaginationResponse<EdsSmevFailure>> GetEdsSmevFailuresAsync(PaginationRequest request);
    }
}
