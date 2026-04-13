using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.PostgreSqlDataAccess.Extensions
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
            QueryObject queryObject)
        {
            try
            {
                if (spanBuilder.IsEnabled == false)
                {
                    return spanBuilder;
                }

                spanBuilder.WithTag("QueryObject", new
                {
                    Sql = queryObject.Sql,
                    Params = queryObject.QueryParams?.QueryParamsToJson(),
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
            QueryObjectWithDynamicParams queryObject)
        {
            try
            {
                if (spanBuilder.IsEnabled == false)
                {
                    return spanBuilder;
                }

                spanBuilder.WithTag("QueryObject", new
                {
                    Sql = queryObject.Sql,
                    Params = queryObject.DynamicParams?.QueryParamsToJson(),
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
            ICollection<QueryObject> queryObjectCollection)
        {
            if (spanBuilder.IsEnabled == false)
            {
                return spanBuilder;
            }

            foreach (var queryObject in queryObjectCollection)
            {
                WithQueryObject(spanBuilder, queryObject);
            }

            return spanBuilder;
        }

        internal static IAuditSpanBuilder WithQueryObject(
            this IAuditSpanBuilder spanBuilder,
            ICollection<QueryObjectWithDynamicParams> queryObjectCollection)
        {
            if (spanBuilder.IsEnabled == false)
            {
                return spanBuilder;
            }

            foreach (var queryObject in queryObjectCollection)
            {
                WithQueryObject(spanBuilder, queryObject);
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
}