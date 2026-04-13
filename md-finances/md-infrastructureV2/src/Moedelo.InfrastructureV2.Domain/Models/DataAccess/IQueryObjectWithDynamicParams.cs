using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.Domain.Models.DataAccess;

public interface IQueryObjectWithDynamicParams : IQueryObjectBase
{
    List<DynamicParam> DynamicParams { get; }
}
