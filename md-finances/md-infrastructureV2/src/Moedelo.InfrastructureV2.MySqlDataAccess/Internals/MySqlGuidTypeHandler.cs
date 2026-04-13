using System;
using System.Data;
using Dapper;

namespace Moedelo.InfrastructureV2.MySqlDataAccess.Internals;

public class MySqlGuidTypeHandler : SqlMapper.TypeHandler<Guid?>
{
    public override void SetValue(IDbDataParameter parameter, Guid? guid)
    {
        parameter.Value = guid.HasValue ? guid.Value : (Guid?)null;
    }

    public override Guid? Parse(object value)
    {
        if (value == null)
            return null;
        if (Guid.TryParse(value.ToString(), out var g))
            return g;
        else
            return null;
    }
}