using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills.Dto;
using Moedelo.Docs.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using TaxationSystemType = Moedelo.Money.Enums.TaxationSystemType;

namespace Moedelo.Money.Providing.Business.PrimaryDocuments
{
    [InjectAsSingleton(typeof(SalesWaybillReader))]
    internal class SalesWaybillReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IWaybillApiClient waybillApiClient;

        public SalesWaybillReader(
            IExecutionInfoContextAccessor contextAccessor,
            IWaybillApiClient waybillApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.waybillApiClient = waybillApiClient;
        }

        public async Task<SalesWaybill[]> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Array.Empty<SalesWaybill>();
            }
            var context = contextAccessor.ExecutionInfoContext;
            var waybills = await waybillApiClient.GetByBaseIdsAsync(context.FirmId, context.UserId, baseIds);
            return waybills.Where(x => x.Direction == PrimaryDocTransferDirection.Outgoing)
                .Select(MapWaybill)
                .ToArray();
        }

        private SalesWaybill MapWaybill(WaybillDto dto)
        {
            return new SalesWaybill
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Items.Sum(x => x.SumWithNds),
                KontragentId = dto.KontragentId,
                KontragentAccountCode = (SyntheticAccountCode)dto.KontragentAccountCode,
                TaxationSystemType = (TaxationSystemType?)dto.TaxationSystemType,
                ForgottenDocumentDate = dto.ForgottenDocumentDate,
                ForgottenDocumentNumber = dto.ForgottenDocumentNumber
            };
        }
    }
}
