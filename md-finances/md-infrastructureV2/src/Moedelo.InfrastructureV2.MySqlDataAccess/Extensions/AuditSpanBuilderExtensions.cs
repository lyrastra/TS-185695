using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.MySqlDataAccess.Extensions;

internal static class AuditSpanBuilderExtensions
{
    internal static IAuditSpanBuilder WithConnectionString(
        this IAuditSpanBuilder spanBuilder,
        string connectionString)
    {
            spanBuilder.WithTag("DbConnectionString", connectionString);
            
            return spanBuilder;
        }
        
    internal static IAuditSpanBuilder WithQueryObject(
        this IAuditSpanBuilder spanBuilder,
        QueryObject queryObject)
    {
            try
            {
                spanBuilder.WithTag("QueryObject", new
                {
                    Sql = queryObject.Sql,
                    Params = queryObject.QueryParams?.QueryParamsToJson(),
                    CommandType = queryObject.CommandType,
                    TemporaryTables = queryObject.TemporaryTables,
                });
            }
            catch
            {
                //ignore
            }
            
            return spanBuilder;
        }
        
    internal static IAuditSpanBuilder WithQueryObject(
        this IAuditSpanBuilder spanBuilder,
        QueryObjectWithDynamicParams queryObject)
    {
            try
            {
                spanBuilder.WithTag("QueryObject", new
                {
                    Sql = queryObject.Sql,
                    Params = queryObject.DynamicParams?.QueryParamsToJson(),
                    CommandType = queryObject.CommandType,
                    TemporaryTables = queryObject.TemporaryTables,
                });
            }
            catch
            {
                //ignore
            }
            
            return spanBuilder;
        }

    private static string QueryParamsToJson(this object queryParams)
    {
            try
            {
                return queryParams?.ToJsonString();
            }
            catch
            {
                return null;
            }
        }
}