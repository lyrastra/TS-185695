using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.DataAccess.Extensions
{
    internal static class AuditSpanBuilderExtensions
    {
        private static readonly MdSerializationSettings JsonSerializationSettings = new MdSerializationSettings
        {
            MaskPropertiesByAttribute = true,
            MaskGenericSensitiveProperties = true
        };

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
            IEnumerable<IQueryObject> queryObjectCollection)
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
            ICollection<IQueryObjectWithDynamicParams> queryObjectCollection)
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
            const int maxQueryParamsDump = 4 * 1024;

            try
            {
                var jsonString = queryParams.ToJsonString(JsonSerializationSettings);

                if (jsonString.Length > maxQueryParamsDump)
                {
                    return jsonString.Substring(0, maxQueryParamsDump);
                }

                return jsonString;
            }
            catch
            {
                return null;
            }
        }
    }
}