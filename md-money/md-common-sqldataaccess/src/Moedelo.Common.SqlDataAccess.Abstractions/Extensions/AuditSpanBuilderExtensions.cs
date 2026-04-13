using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.SqlDataAccess.Abstractions.Extensions
{
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
            IQueryObject queryObject)
        {
            try
            {
                if (spanBuilder.IsEnabled == false)
                {
                    return spanBuilder;
                }
            
                spanBuilder.WithTag("QueryObject", new
                {
                    TemporaryTables = queryObject.TemporaryTables.DumpTemporaryTables(),
                    Sql = queryObject.Sql,
                    Params = queryObject.QueryParams,
                    CommandType = queryObject.CommandType,
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
            IQueryObjectWithDynamicParams queryObject)
        {
            try
            {
                if (spanBuilder.IsEnabled == false)
                {
                    return spanBuilder;
                }
                
                spanBuilder.WithTag("QueryObject", new
                {
                    TemporaryTables = queryObject.TemporaryTables.DumpTemporaryTables(),
                    Sql = queryObject.Sql,
                    Params = queryObject.DynamicParams,
                    CommandType = queryObject.CommandType,
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
            IBulkCopyQueryObject queryObject)
        {
            try
            {
                if (spanBuilder.IsEnabled == false)
                {
                    return spanBuilder;
                }
                
                spanBuilder.WithTag("QueryObject", new
                {
                    Name = queryObject.Name,
                    Data = queryObject.DataTable.DumpDataTableRows()
                });
            }
            catch
            {
                //ignore
            }

            return spanBuilder;
        }
    }
}