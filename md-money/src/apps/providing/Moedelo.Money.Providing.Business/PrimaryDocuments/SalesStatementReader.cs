using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;

namespace Moedelo.Money.Providing.Business.PrimaryDocuments
{
    [InjectAsSingleton(typeof(SalesStatementReader))]
    internal class SalesStatementReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IOutgoingStatementClient statementClient;

        public SalesStatementReader(
            IExecutionInfoContextAccessor contextAccessor,
            IOutgoingStatementClient statementClient)
        {
            this.contextAccessor = contextAccessor;
            this.statementClient = statementClient;
        }

        public async Task<SalesStatement[]> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Array.Empty<SalesStatement>();
            }
            var context = contextAccessor.ExecutionInfoContext;
            var statements = await statementClient.GetByBaseIdsAsync(context.FirmId, context.UserId, baseIds);
            return statements.Select(MapStatement).ToArray();
        }

        private SalesStatement MapStatement(SalesStatementDocDto dto)
        {
            return new SalesStatement
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                KontragentId = dto.KontragentId,
                KontragentAccountCode = (SyntheticAccountCode)dto.KontragentAccountCode,
                TaxationSystemType = (TaxationSystemType?)dto.TaxationSystemType
            };
        }
    }
}
