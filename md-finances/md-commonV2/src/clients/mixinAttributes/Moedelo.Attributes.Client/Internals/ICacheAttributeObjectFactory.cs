using Moedelo.Common.Enums.Enums.mixinAttributes;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Attributes.Client.Internals
{
    public interface ICacheAttributeObjectFactory : IDI
    {
        CacheAttributeObject Get(AttributeObjectType type);
    }
}