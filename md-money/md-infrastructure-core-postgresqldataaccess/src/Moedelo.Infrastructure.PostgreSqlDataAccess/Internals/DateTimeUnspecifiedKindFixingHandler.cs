using System;
using System.Data;
using Dapper;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Internals;

internal sealed class DateTimeUnspecifiedKindFixingHandler : SqlMapper.TypeHandler<DateTime>
{
    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = value;
    }

    public override DateTime Parse(object value)
    {
        var dtValue = (DateTime)value;

        if (dtValue.Kind == DateTimeKind.Unspecified)
        {
            return DateTime.SpecifyKind(dtValue, DateTimeKind.Local);
        }

        return dtValue;
    }
}
