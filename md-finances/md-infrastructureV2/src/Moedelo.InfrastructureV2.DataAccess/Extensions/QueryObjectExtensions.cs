using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using Dapper;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.InfrastructureV2.DataAccess.Extensions;

internal static class QueryObjectExtensions
{
    internal static CommandDefinition ToDapperCommandDefinition(
        this IQueryObject queryObject,
        QuerySetting querySettings,
        CancellationToken cancellationToken)
    {
        var queryParams = queryObject.GetDapperCommandDefinitionParameters();
        
        return new CommandDefinition(
            queryObject.Sql,
            queryParams,
            querySettings.Transaction,
            querySettings.Timeout,
            queryObject.CommandType,
            cancellationToken: cancellationToken);
    }

    internal static object GetDapperCommandDefinitionParameters(
        this IQueryObject queryObject)
    {
        return GetQueryParams(queryObject.QueryParams);
    }

    private static object GetQueryParams(object queryParams)
    {
        if (queryParams == null)
        {
            return null;
        }

        if (queryParams is IEnumerable<object> list)
        {
            return list.Select(PrepareQueryParams).ToList();
        }

        return PrepareQueryParams(queryParams);
    }

    private static object PrepareQueryParams(object queryParams)
    {
        if (queryParams is IDictionary<string, object> dictionary)
        {
            return PrepareQueryParams(dictionary);
        }

        var result = new ExpandoObject();
        var valueType = queryParams.GetType();
        var props = valueType.GetProperties();

        foreach (var prop in props)
        {
            var propValue = prop.GetValue(queryParams, null);
            propValue = propValue.ChangeIfTvp();
            ((IDictionary<string, object>)result).Add(prop.Name, propValue);
        }

        return result;
    }

    private static object PrepareQueryParams(IDictionary<string, object> queryParams)
    {
        var result = (IDictionary<string, object>)new ExpandoObject();

        foreach (var keyValuePair in queryParams)
        {
            var value = keyValuePair.Value.ChangeIfTvp();
            result.Add(keyValuePair.Key, value);
        }

        return result;
    }
}
