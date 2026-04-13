using System;
using System.Data;
using Dapper;
using Npgsql;
using NpgsqlTypes;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Internals;

internal sealed class PassThroughHandler<T> : SqlMapper.TypeHandler<T>
{
    /// <summary>Npgsql database type being handled</summary>
    private readonly NpgsqlDbType dbType;

    /// <summary>Constructor</summary>
    /// <param name="dbType">Npgsql database type being handled</param>
    public PassThroughHandler(NpgsqlDbType dbType)
    {
        this.dbType = dbType;
    }

    public override void SetValue(IDbDataParameter parameter, T value)
    {
        parameter.Value = value;
        parameter.DbType = DbType.Object;

        if (parameter is NpgsqlParameter npgsqlParam)
        {
            npgsqlParam.NpgsqlDbType = dbType;
        }
    }

    public override T Parse(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return default(T);
        }

        if (value is not T typed)
        {
            throw new ArgumentException($"Unable to convert {value.GetType().FullName} to {typeof(T).FullName}", nameof(value));
        }

        return typed;
    }
}
