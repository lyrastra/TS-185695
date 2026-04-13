using System.Collections.Generic;
using System.Linq;
using Moedelo.Finances.Domain.Models.Money.Operations.CashOrders;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Finances.DataAccess.Money.Operations.CashOrders
{
    internal static class QueryBuilder
    {
        internal static QueryObject GetBudgetaryPaymentsByQueryParamsQuery(int firmId, BudgetaryCashOrderOperationQueryParams queryParams)
        {
            var builder = BuildSql.From(CashOrderOperationQueries.Get);

            builder.IncludeBlock("KbkJoin");
            ApplyWhereStatements(builder, queryParams);
            
            var operationsType = new[]
            {
                OperationType.CashOrderBudgetaryPayment
            };
            
            return new QueryObject(builder.ToString(), Map(firmId, queryParams, operationsType));
        }
        
        internal static QueryObject GetBudgetaryPaymentsWithUnifiedTaxPayments(int firmId, BudgetaryCashOrderOperationQueryParams queryParams)
        {
            var builder = BuildSql.From(CashOrderOperationQueries.GetWithUnifiedTaxPayments);

            builder.IncludeBlock("KbkJoin")
                   .IncludeBlock("UtpKbkJoin");
            ApplyWhereStatements(builder, queryParams);
            
            var operationsType = new[]
            {
                OperationType.CashOrderBudgetaryPayment, 
                OperationType.CashOrderOutgoingUnifiedBudgetaryPayment
            };
            
            return new QueryObject(builder.ToString(), Map(firmId, queryParams, operationsType));
        }

        private static void ApplyWhereStatements(BuildSql sql, BudgetaryCashOrderOperationQueryParams queryParams)
        {
            sql.IncludeLineIf("PeriodFilter", (queryParams.StartDate.HasValue && queryParams.EndDate.HasValue))
               .IncludeLineIf("KbkFilter", queryParams.KbkId.HasValue)
               .IncludeLineIf("KbkPaymentType", queryParams.KbkPaymentType.HasValue)
               .IncludeLineIf("PaymentDirection", queryParams.PaymentDirection.HasValue)
               .IncludeLineIf("BudgetaryTaxesAndFees", queryParams.BudgetaryTaxesAndFees?.Count > 0)
               .IncludeLineIf("BudgetaryPeriodType", queryParams.BudgetaryPeriodType.HasValue)
               .IncludeLineIf("BudgetaryPeriodYear", queryParams.BudgetaryYear.HasValue)
               .IncludeLineIf("PatentId", queryParams.PatentId.HasValue);
        }

        private static object Map(int firmId, BudgetaryCashOrderOperationQueryParams queryParams, IEnumerable<OperationType> operationsType)
        {
            return new
            {
                firmId,
                OperationsType = operationsType.Cast<int>().ToIntListTVP(),
                queryParams.StartDate,
                queryParams.EndDate,
                queryParams.PaymentDirection,
                BudgetaryTaxesAndFees = queryParams.BudgetaryTaxesAndFees.ToIntListTVP(),
                queryParams.KbkPaymentType,
                queryParams.KbkId,
                BudgetaryPeriodYear = queryParams.BudgetaryYear,
                queryParams.BudgetaryPeriodType,
                queryParams.PatentId
            };
        }
    }
}