using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Docs.Dto.Docs;

namespace Moedelo.Docs.Client.Upd
{
    public interface IUpdProductUnionApiClient : IDI
    {
        /// <summary>
        /// Объединить продукты, используемые в УПД
        /// </summary>
        Task UnionProductAsync(int firmId, int userId, ProductUnionDto productUnionModel);
    }
}