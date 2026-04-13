using System;
using System.Collections.Generic;
using System.Data;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.InfrastructureV2.MySqlDataAccess;

public sealed class QueryObjectWithDynamicParams
{
    public QueryObjectWithDynamicParams(
        string sql, 
        IReadOnlyCollection<DynamicParam> dynamicParams,
        IReadOnlyCollection<TemporaryTable> temporaryTables = null,
        CommandType? commandType = null)
    {
        if (string.IsNullOrEmpty(sql))
        {
            throw new ArgumentNullException(nameof(sql));
        }

        if (dynamicParams == null || dynamicParams.Count == 0)
        {
            throw new ArgumentNullException(nameof(dynamicParams));
        }

        Sql = sql;
        DynamicParams = dynamicParams;
        TemporaryTables = temporaryTables;
        CommandType = commandType;
    }

    public string Sql { get; }

    public IReadOnlyCollection<DynamicParam> DynamicParams { get; }
        
    public IReadOnlyCollection<TemporaryTable> TemporaryTables { get; }

    public CommandType? CommandType { get; }
}