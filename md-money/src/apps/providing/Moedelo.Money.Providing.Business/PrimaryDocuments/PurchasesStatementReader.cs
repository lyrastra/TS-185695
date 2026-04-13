using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Statements;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Statements.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Documents
{
    [InjectAsSingleton(typeof(PurchasesStatementReader))]
    class PurchasesStatementReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IIncomingStatementClient statementClient;
        private readonly ILinksClient linksClient;

        public PurchasesStatementReader(
            IExecutionInfoContextAccessor contextAccessor,
            IIncomingStatementClient statementClient,
            ILinksClient linksClient)
        {
            this.contextAccessor = contextAccessor;
            this.statementClient = statementClient;
            this.linksClient = linksClient;
        }

        public async Task<PurchasesStatement[]> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Array.Empty<PurchasesStatement>();
            }
            var context = contextAccessor.ExecutionInfoContext;
            var statements = await statementClient.GetByBaseIdsWithItemsAsync(context.FirmId, context.UserId, baseIds);

            var linksByDocuments = await linksClient.GetLinksWithDocumentsAsync(baseIds);

            var result = new List<PurchasesStatement>(statements.Length);
            foreach (var statement in statements)
            {
                var model = MapStatement(statement.Document, statement.Items);

                if (linksByDocuments.TryGetValue(statement.Document.DocumentBaseId, out var links))
                {
                    model.IsCompensated = links.Any(x => x.Document.Type == LinkedDocumentType.MiddlemanContract || x.Document.Type == LinkedDocumentType.Project);
                }

                result.Add(model);
            }
            return result.ToArray();
        }

        private static PurchasesStatement MapStatement(
            PurchasesStatementDocDto dto,
            IReadOnlyList<PurchasesStatementItemResponseDto> dtoItems)
        {
            return new PurchasesStatement
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                KontragentId = dto.KontragentId,
                KontragentAccountCode = (SyntheticAccountCode)dto.KontragentAccountCode,
                TaxationSystemType = (Enums.TaxationSystemType?)dto.TaxationSystemType,
                IsFromFixedAssetInvestment = dto.IsFromFixedAssetInvestment,
                TaxPostingType = (Enums.ProvidePostingType)dto.TaxPostingType,
                Items = dtoItems.Select(i => new PurchasesStatementItem
                {
                    SumWithNds = i.SumWithNds,
                    SumWithoutNds = i.SumWithoutNds,
                }).ToArray()
            };
        }
    }
}
