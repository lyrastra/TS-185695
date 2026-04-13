#nullable enable
namespace Moedelo.InfrastructureV2.Domain.Models.DataAccess;

public interface IQueryObject : IQueryObjectBase
{
    object? QueryParams { get; }
}
