using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;

namespace Moedelo.Money.Providing.Business.PrimaryDocuments
{
    [InjectAsSingleton(typeof(SalesUpdReader))]
    internal class SalesUpdReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISalesUpdApiClient updApiClient;

        public SalesUpdReader(
            IExecutionInfoContextAccessor contextAccessor,
            ISalesUpdApiClient updApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.updApiClient = updApiClient;
        }

        public async Task<SalesUpd[]> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Array.Empty<SalesUpd>();
            }
            var context = contextAccessor.ExecutionInfoContext;
            var upds = await updApiClient.GetWithItemsAsync(context.FirmId, context.UserId, baseIds);
            return upds.Select(MapUpd)
                .ToArray();
        }

        private SalesUpd MapUpd(SalesUpdWithItemsDto dto)
        {
            return new SalesUpd
            {
                DocumentBaseId = dto.Upd.DocumentBaseId,
                Date = dto.Upd.Date.Date,
                Number = dto.Upd.Number,
                KontragentId = dto.Upd.KontragentId,
                KontragentAccountCode = SyntheticAccountCode._62_01, // на момент написания в УПД нет выбора счета контрагента
                TaxationSystemType = (TaxationSystemType?)dto.Upd.TaxSystem,
                Sum = dto.Items.Sum(x => x.SumWithNds),
                ForgottenDocumentDate = dto.Upd.ForgottenDocumentDate,
                ForgottenDocumentNumber = dto.Upd.ForgottenDocumentNumber,
            };
        }
    }
}
