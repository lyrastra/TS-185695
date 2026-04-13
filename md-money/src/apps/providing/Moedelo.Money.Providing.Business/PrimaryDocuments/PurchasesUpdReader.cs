using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Documents
{
    [InjectAsSingleton(typeof(PurchasesUpdReader))]
    internal class PurchasesUpdReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPurchasesUpdApiClient updApiClient;

        public PurchasesUpdReader(
            IExecutionInfoContextAccessor contextAccessor,
            IPurchasesUpdApiClient updApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.updApiClient = updApiClient;
        }

        public async Task<PurchasesUpd[]> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Array.Empty<PurchasesUpd>();
            }
            var context = contextAccessor.ExecutionInfoContext;
            var upds = await updApiClient.GetWithItemsAsync(context.FirmId, context.UserId, baseIds).ConfigureAwait(false);

            var result = upds.Select(MapUpd).ToArray();
            return result.ToArray();
        }

        private PurchasesUpd MapUpd(PurchasesUpdWithItemsDto dto)
        {
            return new PurchasesUpd
            {
                DocumentBaseId = dto.Upd.DocumentBaseId,
                Date = dto.Upd.Date.Date,
                Number = dto.Upd.Number,
                KontragentId = dto.Upd.KontragentId,
                KontragentAccountCode = SyntheticAccountCode._60_01, // на момент написания в УПД нет выбора счета контрагента
                TaxationSystemType = (Enums.TaxationSystemType?)dto.Upd.TaxSystem,
                Sum = dto.Items.Sum(x => x.SumWithNds),
                ForgottenDocumentNumber = dto.Upd.ForgottenDocumentNumber,
                ForgottenDocumentDate = dto.Upd.ForgottenDocumentDate,
                Items = dto.Items.Select(MapUpdItem).ToArray(),
                TaxPostingType = (Enums.ProvidePostingType)dto.Upd.TaxPostingType
            };
        }

        private UpdItem MapUpdItem(PurchasesUpdItemDto dto)
        {
            return new UpdItem
            {
                Type = dto.ItemType,
                StockProductId = dto.StockProductId,
                SumWithNds = dto.SumWithNds,
                SumWithoutNds = dto.SumWithoutNds
            };
        }
    }
}
