using System.Threading;
using Dapper;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.InfrastructureV2.DataAccess.Extensions;

internal readonly struct CommandDefinitionDynamicParameters
{
    public CommandDefinitionDynamicParameters(CommandDefinition commandDefinition, DynamicParameters dynamicParameters)
    {
        CommandDefinition = commandDefinition;
        DynamicParameters = dynamicParameters;
    }

    public CommandDefinition CommandDefinition { get; }
    public DynamicParameters DynamicParameters { get; }

    public void Deconstruct(out CommandDefinition commandDefinition, out DynamicParameters dynamicParameters)
    {
        commandDefinition = CommandDefinition;
        dynamicParameters = DynamicParameters;
    }
}

internal static class QueryObjectWithDynamicParamsExtensions
{
    internal static CommandDefinition ToDapperCommandDefinition(
        this IQueryObjectWithDynamicParams queryObject,
        QuerySetting querySettings,
        CancellationToken cancellationToken)
    {
        var dynamicParams = queryObject.GetDynamicParameters();
        
        return new CommandDefinition(
            queryObject.Sql,
            dynamicParams,
            querySettings.Transaction,
            querySettings.Timeout,
            queryObject.CommandType,
            cancellationToken: cancellationToken);
    }

    internal static CommandDefinitionDynamicParameters ToDapperCommandDefinitionWithDynamicParameters(
        this IQueryObjectWithDynamicParams queryObject,
        QuerySetting querySettings,
        CancellationToken cancellationToken)
    {
        var dynamicParams = queryObject.GetDynamicParameters();
        var commandDefinition = new CommandDefinition(
            queryObject.Sql,
            dynamicParams,
            querySettings.Transaction,
            querySettings.Timeout,
            queryObject.CommandType,
            cancellationToken: cancellationToken);
        
        return new (commandDefinition, dynamicParams);
    }

    internal static DynamicParameters GetDynamicParameters(this IQueryObjectWithDynamicParams queryObject)
    {
        var dynamicParams = new DynamicParameters();

        foreach (var param in queryObject.DynamicParams)
        {
            dynamicParams.Add(param.Name, param.Value.ChangeIfTvp(), param.DbType, param.Direction, param.Size,
                param.Precision, param.Scale);
        }

        return dynamicParams;
    }
}
