using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AttributeLinks.Client.Internals
{
    public interface ICacheAttributeLinkFactory : IDI
    {
        Task<CacheAttributeLink> GetAsync(byte type);
    }
}