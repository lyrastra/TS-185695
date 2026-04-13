using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Extensions.Finances.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.ClosedPeriods;
using Moedelo.Finances.Domain.Models.ClosedPeriods;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.ClosedPeriods
{
    [InjectAsSingleton(typeof(IClosedPeriodsDao))]
    public class ClosedPeriodsDao(IMoedeloReadOnlyDbExecutor dbExecutor) : IClosedPeriodsDao
    {
        public Task<DateTime?> GetLastClosedDateAsync(int firmId, CancellationToken ctx)
        {
            var query = new QueryObject(Sql.GetLastClosedDate, new { firmId })
                .WithAuditTrailSpanName("ClosedPeriodsDao.GetLastClosedDate");
            return dbExecutor.FirstOrDefaultAsync<DateTime?>(query, cancellationToken: ctx);
        }

        public Task<List<MoneyDocumentsCount>> GetNonProvidedInAccountingOrderCountsAsync(int firmId,
            DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var query = BuildSql.From(Sql.GetNonProvidedInAccountingOrderCounts)
                .IncludeBlock("CountCondition")
                .ToQueryObject(GetNonProvidedInAccountingQueryParams(firmId, startDate, endDate))
                .WithAuditTrailSpanName("ClosedPeriodsDao.GetNonProvidedInAccountingOrderCounts");

            return dbExecutor.QueryAsync<MoneyDocumentsCount>(query, cancellationToken: cancellationToken);
        }

        public Task<List<MoneyDocument>> GetNonProvidedInAccountingOrdersAsync(int firmId, DateTime startDate,
            DateTime endDate, CancellationToken cancellationToken)
        {
            var query = BuildSql.From(Sql.GetNonProvidedInAccountingOrderCounts)
                .IncludeBlock("DocumentCondition")
                .ToQueryObject(GetNonProvidedInAccountingQueryParams(firmId, startDate, endDate))
                .WithAuditTrailSpanName("ClosedPeriodsDao.GetNonProvidedInAccountingOrders");

            return dbExecutor.QueryAsync<MoneyDocument>(query, cancellationToken: cancellationToken);
        }

        private static object GetNonProvidedInAccountingQueryParams(int firmId, DateTime startDate, DateTime endDate)
        {
            return new
            {
                firmId,
                startDate,
                endDate,
                paymentOrderType = (int)AccountingDocumentType.PaymentOrder,
                incomingCashOrder = (int)AccountingDocumentType.IncomingCashOrder,
                outcomingCashOrder = (int)AccountingDocumentType.OutcomingCashOrder,
                emptyDocumentType = 0,
                defaultOperationState = OperationState.Default,
                badOperationStates = OperationStateExtensions.BadOperationStates.Cast<int>().ToIntListTVP(),
                excludeCashOperationTypes = new []
                {
                    (int) OperationType.CashOrderIncomingContributionOfOwnFunds
                }
            };
        }
    }
}