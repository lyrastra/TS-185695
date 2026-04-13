using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CommonV2.Auth.Basic.WebApi.Adapter
{
    public interface IBasicAuthValidator : IDI
    {
        Task<bool> ValidateAsync(string user, string passwd);
    }
}