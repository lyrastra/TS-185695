using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills.Dto;
using Moedelo.Docs.Enums;
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
    [InjectAsSingleton(typeof(PurchasesWaybillReader))]
    internal class PurchasesWaybillReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IWaybillApiClient waybillApiClient;
        private readonly ILinksClient linksClient;

        public PurchasesWaybillReader(
            IExecutionInfoContextAccessor contextAccessor,
            IWaybillApiClient waybillApiClient,
            ILinksClient linksClient)
        {
            this.contextAccessor = contextAccessor;
            this.waybillApiClient = waybillApiClient;
            this.linksClient = linksClient;
        }

        public async Task<PurchasesWaybill[]> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Array.Empty<PurchasesWaybill>();
            }
            var context = contextAccessor.ExecutionInfoContext;
            var waybills = await waybillApiClient.GetByBaseIdsAsync(context.FirmId, context.UserId, baseIds);
            var incomingWaybills = waybills.Where(x => x.Direction == PrimaryDocTransferDirection.Incoming)
                .ToArray();

            var linksByDocuments = await linksClient.GetLinksWithDocumentsAsync(baseIds);

            var result = new List<PurchasesWaybill>(incomingWaybills.Length);
            foreach (var waybill in incomingWaybills)
            {
                var model = MapWaybill(waybill);
                if (linksByDocuments.TryGetValue(waybill.DocumentBaseId, out var links))
                {
                    model.IsCompensated = links.Any(x => x.Document.Type == LinkedDocumentType.MiddlemanContract || x.Document.Type == LinkedDocumentType.Project);
                }
                result.Add(model);
            }

            return result.ToArray();
        }

        private PurchasesWaybill MapWaybill(WaybillDto dto)
        {
            return new PurchasesWaybill
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                KontragentId = dto.KontragentId,
                KontragentAccountCode = (SyntheticAccountCode)dto.KontragentAccountCode,
                TaxationSystemType = (Enums.TaxationSystemType?)dto.TaxationSystemType,
                Sum = dto.Items.Sum(x => x.SumWithNds),
                Items = dto.Items.Select(MapWaybillItem).ToArray(),
                ForgottenDocumentNumber = dto.ForgottenDocumentNumber,
                ForgottenDocumentDate = dto.ForgottenDocumentDate,
                IsFromFixedAssetInvestment = dto.IsFromFixedAssetInvestment,
                TaxPostingType = (Enums.ProvidePostingType)dto.TaxPostingType
            };
        }

        private WaybillItem MapWaybillItem(WaybillItemDto dto)
        {
            return new WaybillItem
            {
                StockProductId = dto.StockProductId,
                SumWithNds = dto.SumWithNds,
                SumWithoutNds = dto.SumWithoutNds
            };
        }
    }
}
