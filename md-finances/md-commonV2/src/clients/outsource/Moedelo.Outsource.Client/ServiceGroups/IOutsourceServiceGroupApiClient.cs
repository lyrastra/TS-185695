using Moedelo.Outsource.Dto.ServiceGroups;
using System.Threading.Tasks;

namespace Moedelo.Outsource.Client.ServiceGroups;

public interface IOutsourceServiceGroupApiClient
{
    Task<int> CreateAsync(int accountId, ServiceGroupPostDto data);
}