using System.Threading.Tasks;
using Moedelo.Money.Registry.Business.Models;

namespace Moedelo.Money.Registry.Business.Abstractions
{
    public interface IBalanceMasterService
    {
        Task<BalanceMasterStatus> GetStatusAsync();
    }
}